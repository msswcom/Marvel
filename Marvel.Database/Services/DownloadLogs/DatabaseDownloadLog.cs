using Marvel.Database.Context;
using Marvel.Database.Models;

namespace Marvel.Database.Services.DownloadLogs
{
    public class DatabaseDownloadLog : IDatabaseDownloadLog
    {
        private readonly MarvelContext context;

        public DatabaseDownloadLog(MarvelContext context)
        {
            this.context = context;
        }

        public void Save(DownloadLog downloadLog)
        {
            context.DownloadLog.Add(downloadLog);

            context.SaveChanges();
        }
    }
}
