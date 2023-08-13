
namespace Marvel.Models.Pagination
{
    public class MessageData<T> : Message
    {
        public PaginationItems<T>? Data { get; set; }
    }
}
