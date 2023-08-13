using Marvel.Services.Models;

namespace Marvel.Services.CharactersByComic
{
    public class CharactersByComicService : ICharactersByComicService
    {
        private readonly ICharactersByComicPaginationService charactersByComicPaginationService;

        public CharactersByComicService(ICharactersByComicPaginationService charactersByComicPaginationService)
        {
            this.charactersByComicPaginationService = charactersByComicPaginationService;
        }

        public async Task<List<MarvelCharacter>> ListAsync(int comicId)
        {
            int page = 0;

            var list = new List<MarvelCharacter>();

            var messageData = await charactersByComicPaginationService.PaginationItemsAsync(comicId);

            int count = messageData.data?.total ?? 0;

            if (messageData.data?.results != null)
            {
                list.AddRange(messageData.data.results);

                while ((page + 1) * MarvelPagination.Limit < count)
                {
                    ++page;

                    messageData = await charactersByComicPaginationService.PaginationItemsAsync(page * MarvelPagination.Limit);

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
