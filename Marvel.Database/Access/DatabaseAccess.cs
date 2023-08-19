using Microsoft.Extensions.Configuration;

namespace Marvel.Database.Access
{
    public class DatabaseAccess
    {
        public DatabaseAccess(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Marvel");

            if (String.IsNullOrEmpty(connectionString) || connectionString == "ConnectionString")
            {
                throw new Exception("ConnectionString should be set in appsettings.json.");
            }
        }
    }
}
