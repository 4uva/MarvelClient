using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelSDK.Model
{
    public class ComicList
    {
        public int? Available { get; set; }
        public int? Returned { get; set; }
        public string CollectionURI { get; set; }
        public ComicSummary[] Items { get; set; }
    }
}

