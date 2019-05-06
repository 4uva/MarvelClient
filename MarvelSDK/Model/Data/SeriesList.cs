namespace MarvelSDK.Model
{
    public class SeriesList
    {
        public int? Available { get; set; }
        public int? Returned { get; set; }
        public string CollectionURI { get; set; }
        public SeriesSummary[] Items { get; set; }
    }
}