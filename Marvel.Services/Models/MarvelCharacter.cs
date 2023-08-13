
namespace Marvel.Services.Models
{
    public class MarvelCharacter
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string resourceURI { get; set; }

        // Navigation properties
        public MarvelList? comics { get; set;}
    }
}
