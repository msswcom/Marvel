using Marvel.Database.Models;
using Marvel.Models.Pagination;

namespace Marvel.Database.Services.Characters
{
    public interface IDatabaseCharacters
    {
        public Task<PaginationItems<Character>> SearchAsync(int ID, string? name, int comicID, int page, int size);

        public int Save(List<Character> characters);

        public void Delete();

        public void DeleteIfAny();
    }
}
