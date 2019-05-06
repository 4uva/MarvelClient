using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MarvelSDK.Model;
using Xamarin.Forms;

namespace MarvelClientApp.ViewModels
{
    public class CharacterLinkViewModel : BaseViewModel
    {
        public CharacterLinkViewModel(Link url)
        {
            UrlType = url.Type;
            Url = new Uri(url.Url);
            ClickCommand = new Command(() => Device.OpenUri(Url));
        }

        public string UrlType { get; }
        public Uri Url { get; }
        public ICommand ClickCommand { get; }
    }
}
