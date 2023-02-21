using EntityModelLibrary.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EntityModelLibrary
{
    internal class DbContext : IdentityDbContext<ApplicationUser>
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        // for checking that DI is getting a different instance each time when the dbcontext is injected in the context of a web request
        private Guid _instanceId = Guid.NewGuid();

        public static void AddBaseOptions(DbContextOptionsBuilder<DbContext> builder, string connectionString)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string must be provided", nameof(connectionString));

            builder.UseSqlServer(connectionString, x => { x.EnableRetryOnFailure(); });
        }
    }
}
