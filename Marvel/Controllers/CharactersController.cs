using Marvel.Database.Models;
using Marvel.Database.Services.Characters;
using Marvel.Models.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Marvel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly IDatabaseCharacters databaseCharacters;

        public CharactersController(IDatabaseCharacters databaseCharacters)
        {
            this.databaseCharacters = databaseCharacters;
        }

        [HttpGet]
        public async Task<PaginationItems<Character>> Search(int ID, string? name, int comicID, int page, int size)
        {
            return await databaseCharacters.SearchAsync(ID, name, comicID, page, size);
        }
    }
}