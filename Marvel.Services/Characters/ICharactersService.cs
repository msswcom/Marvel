using Marvel.Services.Models;

namespace Marvel.Services.Characters
{
    public interface ICharactersService
    {
        public Task<List<MarvelCharacter>> ListAsync();
    }
}
