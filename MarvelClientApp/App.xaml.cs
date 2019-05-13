using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MarvelClientApp.Views;
using MarvelClientApp.ViewModels;
using MarvelClientApp.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MarvelClientApp
{
    public partial class App : Application
    {
        CharacterEngine engine;
        const string savedCharactersProperty = "savedCharacters";
        const string downloadedAllProperty = "downloadedAll";
        const string saveDateProperty = "saveDate";

        const string publicKey = "-- ENTER PUBLIC KEY HERE --";
        const string privateKey = "-- ENTER PRIVATE KEY HERE --";
        CharacterListViewModel characterListViewModel;

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new ItemsPage());
        }

        protected override void OnStart()
        {
            LoadEngine();
            characterListViewModel = new CharacterListViewModel(engine);
            MainPage.BindingContext = characterListViewModel;
        }

        protected override void OnSleep()
        {
            SaveEngine();
        }

        bool cleanCache = false; // снаружи чтобы можно было установить при компиляции в true
        void LoadEngine()
        {
            bool cacheExpired = true;
            if (Properties.ContainsKey(saveDateProperty))
            {
                var saveDate = (DateTime)Properties[saveDateProperty];
                if (saveDate >= DateTime.UtcNow.Date)
                    cacheExpired = false;
            }
            bool needRestoreCharacters = !cleanCache && !cacheExpired;

            string savedCharacters = null;
            bool downloadedAll = false;
            if (needRestoreCharacters)
            {
                if (Properties.ContainsKey(savedCharactersProperty))
                    savedCharacters = (string)Properties[savedCharactersProperty];
                if (Properties.ContainsKey(downloadedAllProperty))
                    downloadedAll = (bool)Properties[downloadedAllProperty];
            }
            engine = new CharacterEngine(privateKey, publicKey, savedCharacters, downloadedAll);
            cleanCache = false;
            Debug.WriteLine($"Loaded engine state, {savedCharacters?.Length ?? 0} chars read");
        }

        void SaveEngine()
        {
            bool downloadedAll = !engine.CanDownloadMore();
            string savedCharacters = engine.SerializeCharacters();
            Properties[savedCharactersProperty] = savedCharacters;
            Properties[downloadedAllProperty] = downloadedAll;
            Properties[saveDateProperty] = DateTime.UtcNow.Date;
            Debug.WriteLine($"Saving engine state, {savedCharacters.Length} chars written");
        }
    }
}
