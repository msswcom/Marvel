using Marvel.Services.Models;

namespace Marvel.Services.Comics
{
    public interface IComicsService
    {
        public Task<List<MarvelComic>> ListAsync();
    }
}
