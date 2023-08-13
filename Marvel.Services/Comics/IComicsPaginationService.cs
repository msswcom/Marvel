using Marvel.Services.Models;

namespace Marvel.Services.Comics
{
    public interface IComicsPaginationService
    {
        public Task<MarvelMessageData<MarvelComic>> PaginationItemsAsync(int offset = 0, int limit = MarvelPagination.Limit);
    }
}
