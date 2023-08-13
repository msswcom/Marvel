
namespace Marvel.Database.Models
{
    public class DownloadLog
    {
        public int ID { get; set; }
        public DateTime DownloadDateTime { get; set; }
        public int Code { get; set; }
        public string? Status { get; set; }
        public string? Request { get; set; }
        public string? Response { get; set; }
    }
}
