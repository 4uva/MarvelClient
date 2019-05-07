using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MarvelClientApp.Views;
using MarvelClientApp.ViewModels;

namespace MarvelClientApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        public ItemsPage()
        {
            InitializeComponent();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem is CharacterViewModel characterVM)
            {
                await Navigation.PushAsync(new ItemDetailPage(characterVM));
            }

            if (args.SelectedItem is LoadingFailedViewModel)
            {
                AskForMoreData();
            }

            ItemsListView.SelectedItem = null;
        }

        void OnMoreDataAppearing(object sender, EventArgs e)
        {
            AskForMoreData();
        }

        void AskForMoreData()
        {
            var vm = (CharacterListViewModel)BindingContext;
            vm.LoadMoreItemsCommand.Execute(null);
        }
    }
}