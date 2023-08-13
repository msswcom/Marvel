using Marvel.Database.Context;
using Marvel.Database.Models;
using Marvel.Database.Pagination;
using Marvel.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Marvel.Database.Services.Characters
{
    public class DatabaseCharacters : IDatabaseCharacters
    {
        private readonly MarvelContext context;

        public DatabaseCharacters(MarvelContext context)
        {
            this.context = context;
        }

        public async Task<PaginationItems<Character>> SearchAsync(int ID, string? name, int comicID, int page, int size)
        {
            var characterIDsByComicID = new List<int>();

            if (comicID > 0)
            {
                characterIDsByComicID = context.CharacterComics.Where(o => o.ComicID == comicID)
                    .Select(o => o.CharacterID).ToList();
            }

            var query = context.Characters.Where(o => (ID == 0 || o.ID == ID)
                && (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(o.Name) || o.Name.Contains(name))
                && (comicID == 0 || characterIDsByComicID.Contains(o.ID))).OrderBy(o => o.Name);

            return await DatabasePaginationItems<Character>.PaginationItemsAsync(query, page, size);
        }

        public int Save(List<Character> characters)
        {
            if (characters.DistinctBy(o => o.ID).Count() != characters.Count)
            {
                //exclude duplicate IDs
                characters = (from character in characters
                              where character.ID != 0
                              group character by character.ID into charactersByID
                              select charactersByID.First())
                              .OrderBy(o => o.ID).ToList();
            }

            context.Characters.AddRange(characters);

            context.SaveChanges();

            return characters.Count;
        }

        public void Delete()
        {
            context.Database.ExecuteSql($"delete from Character");
        }

        public void DeleteIfAny()
        {
            if (context.Characters.Any())
            {
                Delete();
            }
        }
    }
}
