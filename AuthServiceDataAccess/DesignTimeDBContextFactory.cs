using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AuthServiceDataAccess
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
    {
        public AuthDbContext CreateDbContext(string[] args)
        {
            string path = Directory.GetCurrentDirectory();

            IConfigurationBuilder builder =
                new ConfigurationBuilder()
                    .SetBasePath(path)
                    .AddJsonFile("appsettings.json");

            IConfigurationRoot config = builder.Build();

            string connectionString = config.GetConnectionString("ConnString");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Could not find connection string named 'ConnString'");
            }

            DbContextOptionsBuilder<AuthDbContext> dbContextOptionsBuilder =
                new DbContextOptionsBuilder<AuthDbContext>();

            AuthDbContext.AddBaseOptions(dbContextOptionsBuilder, connectionString);

            return new AuthDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
