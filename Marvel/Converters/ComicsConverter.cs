using Marvel.Database.Models;
using Marvel.Services.Models;

namespace Marvel.Converters
{
    public class ComicsConverter : IComicsConverter
    {
        public List<Comic> ToList(List<MarvelComic> marvelComics)
        {
            var comics = marvelComics.Select(o => new Comic
            {
                ID = o.id,
                DigitalId = o.digitalId,
                Title = o.title,
                IssueNumber = o.issueNumber,
                VariantDescription = o.variantDescription,
                Description = o.description,
                Upc = o.upc,
                Ean = o.ean,
                Issn = o.issn,
                Format = o.format,
                PageCount = o.pageCount,
                ResourceURI = o.resourceURI
            }).OrderBy(o => o.ID).ToList();

            return comics;
        }
    }
}
