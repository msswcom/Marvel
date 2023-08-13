using System.ComponentModel.DataAnnotations.Schema;

namespace Marvel.Database.Models
{
    public class Comic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public int? DigitalId { get; set; }
        public string? Title { get; set; }
        public double? IssueNumber { get; set; }
        public string? VariantDescription { get; set; }
        public string? Description { get; set; }
        public string? Upc { get; set; }
        public string? Ean { get; set; }
        public string? Issn { get; set; }
        public string? Format { get; set; }
        public int? PageCount { get; set; }
        public string? ResourceURI { get; set; }

        // Navigation properties
        public ICollection<CharacterComic> CharacterComics { get; set;}
    }
}
