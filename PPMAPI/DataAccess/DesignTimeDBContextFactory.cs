using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PPMAPI.DataAccess
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PPMDbContext>
    {
        public PPMDbContext CreateDbContext(string[] args)
        {
            string path = Directory.GetCurrentDirectory();

            IConfigurationBuilder builder =
                new ConfigurationBuilder()
                    .SetBasePath(path)
                    .AddJsonFile("appsettings.json");

            IConfigurationRoot config = builder.Build();

            string connectionString = config.GetConnectionString("APIConnString");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Could not find connection string named 'ConnString'");
            }

            DbContextOptionsBuilder<PPMDbContext> dbContextOptionsBuilder =
                new DbContextOptionsBuilder<PPMDbContext>();

            PPMDbContext.AddBaseOptions(dbContextOptionsBuilder, connectionString);

            return new PPMDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
