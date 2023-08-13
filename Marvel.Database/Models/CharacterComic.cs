
namespace Marvel.Database.Models
{
    public class CharacterComic
    {
        public int ID { get; set; }
        public int CharacterID { get; set; }
        public int ComicID { get; set; }

        // Navigation properties
        public Character Character { get; set; }
        public Comic Comic { get; set; }
    }
}
