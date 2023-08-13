
namespace Marvel.Services.Models
{
    public class MarvelList
    {
        public int available { get; set; }
        public string? collectionURI { get; set; }
        public MarvelSummary[]? items { get; set; }
        public int returned { get; set; }
    }
}
