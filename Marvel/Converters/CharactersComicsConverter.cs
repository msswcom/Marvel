using Marvel.Database.Models;
using Marvel.Models.Extensions;
using Marvel.Services.CharactersByComic;
using Marvel.Services.Models;

namespace Marvel.Converters
{
    public class CharactersComicsConverter : ICharactersComicsConverter
    {
        private readonly ICharactersByComicService charactersByComicService;

        public CharactersComicsConverter(ICharactersByComicService charactersByComicService)
        {
            this.charactersByComicService = charactersByComicService;
        }

        public async Task<List<CharacterComic>> ToListAsync(List<MarvelComic> marvelComics)
        {
            var charactersComics = new List<CharacterComic>();

            foreach (var marvelComic in marvelComics)
            {
                if (marvelComic?.characters?.available > marvelComic?.characters?.returned)
                {
                    var charactersByComic = await charactersByComicService.ListAsync(marvelComic.id);

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
                        foreach (var item in marvelComic?.characters?.items)
                        {
                            if (!String.IsNullOrEmpty(item.resourceURI))
                            {
                                var index = item.resourceURI.LastIndexOf("/");

                                if (item.resourceURI.Length > index + 1)
                                {
                                    int characterID = item.resourceURI.Substring(index + 1).ToInt();

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
