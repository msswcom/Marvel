using Marvel.Database.Models;
using Marvel.Services.Models;

namespace Marvel.Converters
{
    public interface ICharactersComicsConverter
    {
        public Task<List<CharacterComic>> ToListAsync(List<MarvelComic> marvelComics);
    }
}
