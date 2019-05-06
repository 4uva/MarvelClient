using System;
using System.Collections.Generic;
using System.Text;
using MarvelClientApp.ViewModels;
using Xamarin.Forms;

namespace MarvelClientApp.Views
{
    class CharacterListItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CharacterTemplate { get; set; }
        public DataTemplate LoadMoreTemplate { get; set; }
        public DataTemplate LoadingFailedTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (item is CharacterViewModel)
                return CharacterTemplate;
            if (item is LoadingMoreViewModel)
                return LoadMoreTemplate;
            if (item is LoadingFailedViewModel)
                return LoadingFailedTemplate;
            throw new ArgumentException("Unknown VM type: " + item.GetType());
        }
    }
}
