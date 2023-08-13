
namespace Marvel.Models.Pagination
{
    public class PaginationItems<T>
    {
        /// <summary>
        /// 1-based page index
        /// </summary>
        public int Page { get; private set; }
        /// <summary>
        /// Total page count
        /// </summary>
        public int Total { get; private set; }
        /// <summary>
        /// Page size
        /// </summary>
        public int Size { get; private set; }
        /// <summary>
        /// Page items count
        /// </summary>
        public int Count { get; private set; }
        public List<T> Items { get; private set; }

        public PaginationItems(int page, int count, int size, List<T> items)
        {
            Page = page;
            Total = (int)Math.Ceiling(count / (double)size);
            Size = size;
            Count = items.Count;
            Items = items;
        }

        public bool Previous => Page > 1;
        public bool Next => Page < Total;
    }
}
