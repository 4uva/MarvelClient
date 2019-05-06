namespace MarvelSDK.Model
{
    public class EventList
    {
        public int? Available { get; set; }
        public int? Returned { get; set; }
        public string CollectionURI { get; set; }
        public EventSummary[] Items { get; set; }
    }
}