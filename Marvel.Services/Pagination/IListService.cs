
namespace Marvel.Services.Pagination
{
    public interface IListService
    {
        public Task<List<T>> ToListAsync<T>(string url, string sort);
    }
}
