using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using MarvelClientApp.Views;
using MarvelSDK.Model;
using System.Collections.Generic;
using System.Windows.Input;

namespace MarvelClientApp.ViewModels
{
    public class CharacterListViewModel : BaseViewModel
    {
        public ObservableCollection<ListItemViewModel> Items { get; }
        public ICommand LoadMoreItemsCommand { get; }

        CharacterEngine engine;

        public CharacterListViewModel(CharacterEngine engine)
        {
            this.engine = engine;

            LoadMoreItemsCommand = new Command(LoadMoreItemsIfNotBusy, () => IsBusy);

            var currentCharacters = engine.GetCharactersFrom(0);
            var characterVMs = new List<CharacterViewModel>();
            foreach (var character in currentCharacters)
                characterVMs.Add(new CharacterViewModel(character));

            Items = new ObservableCollection<ListItemViewModel>(characterVMs);
            AddMoreIfNeeded();
        }

        void AddMoreIfNeeded()
        {
            if (engine.CanDownloadMore())
                Items.Add(new LoadingMoreViewModel());
        }

        async void LoadMoreItemsIfNotBusy()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            await LoadMoreItems();
            IsBusy = false;
        }

        // не бросает исключений
        async Task LoadMoreItems()
        {
            try
            {
                await engine.DownloadMore();
                int count = Items.Count;
                bool hadExtraItem = count > 0 && !(Items[count - 1] is CharacterViewModel);
                if (hadExtraItem)
                    count -= 1;
                var newItems = engine.GetCharactersFrom(count);
                if (hadExtraItem)
                    Items.RemoveAt(count);

                var newCharacters = engine.GetCharactersFrom(count);
                foreach (var character in newCharacters)
                {
                    var characterVM = new CharacterViewModel(character);
                    Items.Add(characterVM);
                }
                AddMoreIfNeeded();
            }
            catch (DataDownloadException ex)
            {
                Debug.WriteLine($"Exception happened while fetching more items: {ex}");
                int count = Items.Count;
                bool hadExtraItem = count > 0 && !(Items[count - 1] is CharacterViewModel);
                if (hadExtraItem)
                    Items.RemoveAt(count - 1);
                Items.Add(new LoadingFailedViewModel());
            }
            catch (RestartFetchingException)
            {
                Debug.WriteLine($"Content changed! Loading again");
                Items.Clear();
                Items.Add(new LoadingMoreViewModel());
                await LoadMoreItems();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception happened while fetching more items: {ex}");
            }
        }
    }
}