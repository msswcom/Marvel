using Marvel.Database.Models;
using Marvel.Services.Models;

namespace Marvel.Converters
{
    public class CharactersConverter : ICharactersConverter
    {
        public List<Character> ToList(List<MarvelCharacter> marvelCharacters)
        {
            var characters = marvelCharacters.Select(o => new Character
            {
                ID = o.id,
                Name = o.name,
                Description = o.description,
                ResourceURI = o.resourceURI
            }).OrderBy(o => o.ID).ToList();

            return characters;
        }
    }
}
