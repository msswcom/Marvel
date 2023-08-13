using System.ComponentModel.DataAnnotations.Schema;

namespace Marvel.Database.Models
{
    public class Character
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ResourceURI { get; set; }

        // Navigation properties
        public ICollection<CharacterComic> CharacterComics { get; set;}
    }
}
