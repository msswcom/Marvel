using Marvel.Converters;
using Marvel.Database.Models;
using Marvel.Database.Services.Characters;
using Marvel.Database.Services.CharactersComics;
using Marvel.Database.Services.Comics;
using Marvel.Database.Services.DownloadLogs;
using Marvel.Models.Pagination;
using Marvel.Services;
using Marvel.Services.Models;
using Marvel.Services.Pagination;
using Marvel.Services.Sort;
using Microsoft.AspNetCore.Mvc;

namespace Marvel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DownloadController : ControllerBase
    {
        private readonly IListService listService;
        private readonly ICharactersComicsConverter charactersComicsConverter;
        private readonly ICharactersConverter charactersConverter;
        private readonly IComicsConverter comicsConverter;
        private readonly IDatabaseCharactersComics databaseCharactersComics;
        private readonly IDatabaseCharacters databaseCharacters;
        private readonly IDatabaseComics databaseComics;
        private readonly IDatabaseDownloadLog downloadLogWrite;

        public DownloadController(IListService listService,
            ICharactersComicsConverter charactersComicsConverter,
            ICharactersConverter charactersConverter,
            IComicsConverter comicsConverter,
            IDatabaseCharactersComics databaseCharactersComics,
            IDatabaseCharacters databaseCharacters,
            IDatabaseComics databaseComics,
            IDatabaseDownloadLog downloadLogWrite)
        {
            this.listService = listService;
            this.charactersComicsConverter = charactersComicsConverter;
            this.charactersConverter = charactersConverter;
            this.comicsConverter = comicsConverter;
            this.databaseCharactersComics = databaseCharactersComics;
            this.databaseCharacters = databaseCharacters;
            this.databaseComics = databaseComics;
            this.downloadLogWrite = downloadLogWrite;
        }

        [HttpPost]
        public async Task<Message> Download()
        {
            var message = new Message();

            try
            {
                var marvelCharacters = await listService.ToListAsync<MarvelCharacter>(
                    ServiceUrl.Characters,
                    CharacterSort.Name);
                var marvelComics = await listService.ToListAsync<MarvelComic>(
                    ServiceUrl.Comics,
                    ComicSort.Title);

                var characters = charactersConverter.ToList(marvelCharacters);
                var comics = comicsConverter.ToList(marvelComics);
                var charactersComics = await charactersComicsConverter.ToListAsync(marvelComics);

                databaseCharactersComics.DeleteIfAny();
                databaseCharacters.DeleteIfAny();
                databaseComics.DeleteIfAny();

                int charactersCount = databaseCharacters.Save(characters);
                int comicsCount = databaseComics.Save(comics);
                int charactersComicsCount = databaseCharactersComics.Save(charactersComics);

                message.Code = 200;
                message.Status =
                    $"{marvelCharacters.Count} characters and {marvelComics.Count} comics downloaded from Marvel service. "
                    + $"{charactersCount} characters and {comicsCount} comics saved to database. "
                    + $"Duplicate IDs returned by Marvel service were discarded and unique IDs were saved to the database.";

                downloadLogWrite.Save(new DownloadLog
                {
                    DownloadDateTime = DateTime.Now,
                    Code = message.Code,
                    Status = message.Status
                });
            }
            catch (Exception? exception)
            {
                message.Code = 500;

                while (exception != null)
                {
                    message.Status += exception.Message + " ";

                    exception = exception.InnerException;
                }

                downloadLogWrite.Save(new DownloadLog
                {
                    DownloadDateTime = DateTime.Now,
                    Code = message.Code,
                    Status = message.Status
                });

                throw;
            }

            return message;
        }
    }
}