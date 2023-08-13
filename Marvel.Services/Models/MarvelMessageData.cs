
namespace Marvel.Services.Models
{
    public class MarvelMessageData<T>
    {
        public int code { get; set; }
        public string? status { get; set; }
        public MarvelPaginationItems<T>? data { get; set; }
    }
}
