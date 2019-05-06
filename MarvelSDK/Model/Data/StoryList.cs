namespace MarvelSDK.Model
{
    public class StoryList
    {
        public int? Available { get; set; }
        public int? Returned { get; set; }
        public string CollectionURI { get; set; }
        public StorySummary[] Items { get; set; }
    }
}