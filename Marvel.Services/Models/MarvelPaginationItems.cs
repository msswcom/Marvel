
namespace Marvel.Services.Models
{
    public class MarvelPaginationItems<T>
    {
        public int offset { get; set; }
        public int limit { get; set; }
        public int total { get; set; }
        public int count { get; set; }
        public T[]? results { get; set; }
    }
}
