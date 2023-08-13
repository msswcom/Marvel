using Marvel.Services.Models;

namespace Marvel.Services.Comics
{
    public class ComicsService : IComicsService
    {
        private readonly IComicsPaginationService comicsPaginationService;

        public ComicsService(IComicsPaginationService comicsPaginationService)
        {
            this.comicsPaginationService = comicsPaginationService;
        }

        public async Task<List<MarvelComic>> ListAsync()
        {
            int page = 0;

            var list = new List<MarvelComic>();

            var messageData = await comicsPaginationService.PaginationItemsAsync();

            int count = messageData.data?.total ?? 0;

            if (messageData.data?.results != null)
            {
                list.AddRange(messageData.data.results);

                while ((page + 1) * MarvelPagination.Limit < count)
                {
                    ++page;

                    messageData = await comicsPaginationService.PaginationItemsAsync(page * MarvelPagination.Limit);

                    if (messageData.data?.results != null)
                    {
                        list.AddRange(messageData.data.results);
                    }
                }
            }

            return list;
        }
    }
}
