using System;
using System.Collections.Generic;
using MarvelSDK.Model;

namespace MarvelClientApp.ViewModels
{
    public class CharacterViewModel : ListItemViewModel
    {
        public CharacterViewModel(Character character)
        {
            Title = character.Name;
            Name = character.Name;
            var imagePath = character.Thumbnail?.Path;
            var imageExt = character.Thumbnail?.Extension;
            if (imagePath != null)
            {
                if (imageExt != null)
                    imagePath = imagePath + "." + imageExt;
                ImageUri = new Uri(imagePath);
            }
            Description = character.Description;
            if (character.Urls != null)
            {
                var links = new List<CharacterLinkViewModel>();
                foreach (var url in character.Urls)
                    links.Add(new CharacterLinkViewModel(url));
                Links = links;
            }
        }

        public string Title { get; }
        public string Name { get; }
        public Uri ImageUri { get; }
        public string Description { get; }
        public IReadOnlyCollection<CharacterLinkViewModel> Links { get; }
    }
}
