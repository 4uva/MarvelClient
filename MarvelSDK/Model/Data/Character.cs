using System;
using System.Collections.Generic;

namespace MarvelSDK.Model
{
   
    public class Character
    {
        public int Id { get; set; } // optional?
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Modified { get; set; }
        public string ResourceURI { get; set; }
        public Link[] Urls { get; set; }
        public Image Thumbnail { get; set; }
        // экономим место
        //public ComicList Comics { get; set; }
        //public StoryList Stories { get; set; }
        //public EventList Events { get; set; }
        //public SeriesList Series { get; set; }
    }
}

