using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelSDK.Model
{
    public class CharacterDataContainer
    {
        public int? Offset { get; set; }
        public int? Limit { get; set; }
        public int? Total { get; set; }
        public int? Count { get; set; }
        public Character [] Results { get; set; }
    }
}
