using Marvel.Database.Access;
using Marvel.Database.Context;
using Marvel.Database.Models;
using Microsoft.Extensions.Configuration;

namespace Marvel.Database.Services.DownloadLogs
{
    public class DatabaseDownloadLog : DatabaseAccess, IDatabaseDownloadLog
    {
        private readonly MarvelContext context;

        public DatabaseDownloadLog(MarvelContext context, IConfiguration configuration) : base(configuration)
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
