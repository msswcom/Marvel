using Marvel.Services.Models;

namespace Marvel.Services.Pagination
{
    public class ListService : IListService
    {
        private readonly IPaginationService paginationService;

        public ListService(IPaginationService paginationService)
        {
            this.paginationService = paginationService;
        }

        public async Task<List<T>> ToListAsync<T>(string url, string sort)
        {
            int page = 0;

            var list = new List<T>();

            var messageData = await paginationService.ToPaginationItemsAsync<T>(url, sort);

            int count = messageData.data?.total ?? 0;

            if (messageData.data?.results != null)
            {
                list.AddRange(messageData.data.results);

                while ((page + 1) * MarvelPagination.Limit < count)
                {
                    ++page;

                    messageData = await paginationService.ToPaginationItemsAsync<T>(url, sort, page * MarvelPagination.Limit);

                    if (messageData.data?.results != null)
                    {
                        list.AddRange(messageData.data.results);
                    }
                    else
                    {
                        throw new Exception("messageData.data?.results null");
                    }
                }
            }

            return list;
        }
    }
}
