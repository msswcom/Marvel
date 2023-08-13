using Marvel.Database.Models;
using Marvel.Database.Services.Comics;
using Marvel.Models.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Marvel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComicsController : ControllerBase
    {
        private readonly IDatabaseComics databaseComics;

        public ComicsController(IDatabaseComics databaseComics)
        {
            this.databaseComics = databaseComics;
        }

        [HttpGet]
        public async Task<PaginationItems<Comic>> Search(int ID, string? name, int characterID, int page, int size)
        {
            return await databaseComics.SearchAsync(ID, name, characterID, page, size);
        }
    }
}