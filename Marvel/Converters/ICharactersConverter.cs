using Marvel.Database.Models;
using Marvel.Services.Models;

namespace Marvel.Converters
{
    public interface ICharactersConverter
    {
        public List<Character> ToList(List<MarvelCharacter> marvelCharacters);
    }
}
