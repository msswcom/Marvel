using Marvel.Database.Access;
using Marvel.Database.Context;
using Marvel.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Marvel.Database.Services.CharactersComics
{
    public class DatabaseCharactersComics : DatabaseAccess, IDatabaseCharactersComics
    {
        private readonly MarvelContext context;

        public DatabaseCharactersComics(MarvelContext context, IConfiguration configuration) : base(configuration)
        {
            this.context = context;
        }

        public int Save(List<CharacterComic> charactersComics)
        {
            if (charactersComics.DistinctBy(o => new { o.CharacterID, o.ComicID }).Count() != charactersComics.Count)
            {
                //exclude duplicate IDs
                charactersComics = (from characterComic in charactersComics
                                    where characterComic.CharacterID != 0
                                    && characterComic.ComicID != 0
                                    group characterComic by new {
                                        characterComic.CharacterID,
                                        characterComic.ComicID
                                    }
                                    into charactersComicsByCharacterIDComicID
                                    select charactersComicsByCharacterIDComicID.First()).ToList();
            }

            context.CharacterComics.AddRange(charactersComics);

            context.SaveChanges();

            return charactersComics.Count;
        }

        public void Delete()
        {
            context.Database.ExecuteSql($"delete from CharacterComic");
        }

        public void DeleteIfAny()
        {
            if (context.CharacterComics.Any())
            {
                Delete();
            }
        }
    }
}
