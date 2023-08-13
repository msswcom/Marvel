
namespace Marvel.Services.Models
{
    public class MarvelComic
    {
        public int id { get; set; }
        public int? digitalId { get; set; }
        public string title { get; set; }
        public double issueNumber { get; set; }
        public string variantDescription { get; set; }
        public string description { get; set; }
        public string upc { get; set; }
        public string ean { get; set; }
        public string issn { get; set; }
        public string format { get; set; }
        public int? pageCount { get; set; }
        public string resourceURI { get; set; }

        // Navigation properties
        public MarvelList? characters { get; set; }
    }
}
