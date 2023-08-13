
namespace Marvel.Models.Extensions
{
    public static class DateTimeExtension
    {
        public static string ToString2(this DateTime date)
        {
            return date.ToString("ddMMyyyyHHmmss");
        }
    }
}
