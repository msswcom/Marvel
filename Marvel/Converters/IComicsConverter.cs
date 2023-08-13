using Marvel.Database.Models;
using Marvel.Services.Models;

namespace Marvel.Converters
{
    public interface IComicsConverter
    {
        public List<Comic> ToList(List<MarvelComic> marvelComics);
    }
}
