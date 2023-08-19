using Marvel.Services.Models;

namespace Marvel.Services.Pagination
{
    public interface IPaginationService
    {
        public Task<MarvelMessageData<T>> ToPaginationItemsAsync<T>(
            string url,
            string sort,
            int offset = 0,
            int limit = MarvelPagination.Limit);
    }
}
