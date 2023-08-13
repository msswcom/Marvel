using Marvel.Database.Context;
using Marvel.Database.Models;
using Marvel.Database.Pagination;
using Marvel.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Marvel.Database.Services.Comics
{
    public class DatabaseComics : IDatabaseComics
    {
        private readonly MarvelContext context;

        public DatabaseComics(MarvelContext context)
        {
            this.context = context;
        }

        public async Task<PaginationItems<Comic>> SearchAsync(int ID, string? title, int characterID, int page, int size)
        {
            var query = context.Comics.Where(o => (ID == 0 || o.ID == ID)
                && (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(o.Title) || o.Title.Contains(title))
                && (characterID == 0 || o.CharacterComics.Select(c => c.CharacterID).Contains(characterID)))
                .OrderBy(o => o.Title);

            return await DatabasePaginationItems<Comic>.PaginationItemsAsync(query, page, size);
        }

        public int Save(List<Comic> comics)
        {
            if (comics.DistinctBy(o => o.ID).Count() != comics.Count)
            {
                //exclude duplicate IDs
                comics = (from comic in comics
                          where comic.ID != 0
                          group comic by comic.ID into comicsByID
                          select comicsByID.First())
                          .OrderBy(o => o.ID).ToList();
            }

            context.Comics.AddRange(comics);

            context.SaveChanges();

            return comics.Count;
        }

        public void Delete()
        {
            context.Database.ExecuteSql($"delete from Comic");
        }

        public void DeleteIfAny()
        {
            if (context.Comics.Any())
            {
                Delete();
            }
        }
    }
}
