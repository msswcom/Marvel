using Marvel.Database.Models;
using Marvel.Models.Extensions;
using Marvel.Services;
using Marvel.Services.Models;
using Marvel.Services.Pagination;
using Marvel.Services.Sort;

namespace Marvel.Converters
{
    public class CharactersComicsConverter : ICharactersComicsConverter
    {
        private readonly IListService listService;

        public CharactersComicsConverter(IListService listService)
        {
            this.listService = listService;
        }

        public async Task<List<CharacterComic>> ToListAsync(List<MarvelComic> marvelComics)
        {
            var charactersComics = new List<CharacterComic>();

            foreach (var marvelComic in marvelComics)
            {
                if (marvelComic?.characters?.available > marvelComic?.characters?.returned)
                {
                    var charactersByComic = await listService.ToListAsync<MarvelCharacter>(
                        String.Format(ServiceUrl.CharactersByComic, marvelComic.id),
                        CharacterSort.Name);

                    charactersComics.AddRange(charactersByComic.Select(o => new CharacterComic
                    {
                        CharacterID = o.id,
                        ComicID = marvelComic.id
                    }).ToList());
                }
                else
                {
                    if (marvelComic?.characters?.items != null)
                    {
                        foreach (var item in marvelComic.characters.items)
                        {
                            if (!String.IsNullOrEmpty(item.resourceURI))
                            {
                                var index = item.resourceURI.LastIndexOf("/");

                                if (item.resourceURI.Length > index + 1)
                                {
                                    int characterID = item.resourceURI[(index + 1)..].ToInt();

                                    if (characterID != 0)
                                    {
                                        charactersComics.Add(new CharacterComic
                                        {
                                            CharacterID = characterID,
                                            ComicID = marvelComic.id
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return charactersComics;
        }
    }
}
