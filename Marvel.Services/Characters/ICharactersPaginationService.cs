using Marvel.Services.Models;

namespace Marvel.Services.Characters
{
    public interface ICharactersPaginationService
    {
        public Task<MarvelMessageData<MarvelCharacter>> PaginationItemsAsync(int offset = 0, int limit = MarvelPagination.Limit);
    }
}
