using Marvel.Services.Models;

namespace Marvel.Services.CharactersByComic
{
    public interface ICharactersByComicService
    {
        public Task<List<MarvelCharacter>> ListAsync(int comicId);
    }
}
