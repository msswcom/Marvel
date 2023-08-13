using Marvel.Database.Models;

namespace Marvel.Database.Services.DownloadLogs
{
    public interface IDatabaseDownloadLog
    {
        public void Save(DownloadLog downloadLog);
    }
}
