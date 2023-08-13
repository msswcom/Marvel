using Marvel.Database.Models;

namespace Marvel.Database.Services.CharactersComics
{
    public interface IDatabaseCharactersComics
    {
        public int Save(List<CharacterComic> charactersComics);

        public void Delete();

        public void DeleteIfAny();
    }
}
