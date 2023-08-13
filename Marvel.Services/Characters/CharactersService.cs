using Marvel.Services.Models;

namespace Marvel.Services.Characters
{
    public class CharactersService : ICharactersService
    {
        private readonly ICharactersPaginationService charactersPaginationService;

        public CharactersService(ICharactersPaginationService charactersPaginationService)
        {
            this.charactersPaginationService = charactersPaginationService;
        }

        public async Task<List<MarvelCharacter>> ListAsync()
        {
            int page = 0;

            var list = new List<MarvelCharacter>();

            var messageData = await charactersPaginationService.PaginationItemsAsync();

            int count = messageData.data?.total ?? 0;

            if (messageData.data?.results != null)
            {
                list.AddRange(messageData.data.results);

                while ((page + 1) * MarvelPagination.Limit < count)
                {
                    ++page;

                    messageData = await charactersPaginationService.PaginationItemsAsync(page * MarvelPagination.Limit);

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
