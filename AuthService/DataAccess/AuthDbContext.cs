using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AuthServiceModelLibrary.ApplicationUser;

namespace AuthService.DataAccess
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        // for checking that DI is getting a different instance each time when the dbcontext is injected in the context of a web request
        private Guid _instanceId = Guid.NewGuid();

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .Ignore(u => u.PhoneNumberConfirmed)
                .Ignore(u => u.PhoneNumber)
                .Ignore(u => u.LockoutEnabled)
                .Ignore(u => u.LockoutEnd)
                .Ignore(u => u.TwoFactorEnabled)
                .Ignore(u => u.NormalizedUserName)
                .Ignore(u => u.NormalizedEmail)
                .Ignore(u => u.EmailConfirmed)
                .Ignore(u => u.SecurityStamp)
                .Ignore(u => u.ConcurrencyStamp)
                .Ignore(u => u.AccessFailedCount)
                .ToTable("Users", "Authentication");

            builder.Entity<IdentityRole>()
                .ToTable("Roles", "Authentication");

            builder.Entity<IdentityUserRole<string>>()
                .ToTable("UserRoles", "Authentication");
        }

        public static void AddBaseOptions(DbContextOptionsBuilder<AuthDbContext> builder, string connectionString)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string must be provided", nameof(connectionString));

            builder.UseSqlServer(connectionString, x => { x.EnableRetryOnFailure(); });
        }
    }
}
