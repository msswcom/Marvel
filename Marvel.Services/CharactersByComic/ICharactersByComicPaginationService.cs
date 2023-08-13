using Marvel.Services.Models;

namespace Marvel.Services.CharactersByComic
{
    public interface ICharactersByComicPaginationService
    {
        public Task<MarvelMessageData<MarvelCharacter>> PaginationItemsAsync(int comicId,
            int offset = 0, int limit = MarvelPagination.Limit);
    }
}
