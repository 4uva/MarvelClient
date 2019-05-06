using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MarvelClientApp.ViewModels;

namespace MarvelClientApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage(CharacterViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();
        }
    }
}