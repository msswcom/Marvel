using Marvel.Database.Models;
using Marvel.Models.Pagination;

namespace Marvel.Database.Services.Comics
{
    public interface IDatabaseComics
    {
        public Task<PaginationItems<Comic>> SearchAsync(int ID, string? name, int characterID, int page, int size);

        public int Save(List<Comic> comics);

        public void Delete();

        public void DeleteIfAny();
    }
}
