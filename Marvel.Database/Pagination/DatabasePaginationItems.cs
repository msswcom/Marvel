using Marvel.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Marvel.Database.Pagination
{
    public class DatabasePaginationItems<T> : PaginationItems<T>
    {
        public DatabasePaginationItems(int page, int count, int size, List<T> items) : base(page, count, size, items)
        {
        }

        public static async Task<PaginationItems<T>> PaginationItemsAsync(IQueryable<T> query, int page, int size)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (size < 1)
            {
                size = DatabasePagination.Size;
            }

            var count = await query.CountAsync();
            var items = await query.Skip((page - 1) * size).Take(size).ToListAsync();

            return new PaginationItems<T>(page, count, size, items);
        }
    }
}
