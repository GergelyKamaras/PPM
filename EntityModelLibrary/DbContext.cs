using EntityModelLibrary.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using EntityModelLibrary.Models.Users.Administrator;
using EntityModelLibrary.Models.Users.Owner;
using EntityModelLibrary.Models.Users.Tenant;

namespace EntityModelLibrary
{
    internal class DbContext : IdentityDbContext<ApplicationUser>
    {
        // for checking that DI is getting a different instance each time when the dbcontext is injected in the context of a web request
        private Guid _instanceId = Guid.NewGuid();

        // DbSets for entity tables
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        public DbContext(DbContextOptions<DbContext> options) : base(options)
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

            builder.Entity<IdentityUserLogin<string>>()
                .ToTable("UserLogins", "Authentication");

            builder.Entity<IdentityUserClaim<string>>()
                .ToTable("UserClaims", "Authentication");

            builder.Entity<IdentityRoleClaim<string>>()
                .ToTable("RoleClaims", "Authentication");

            builder.Entity<IdentityUserToken<string>>()
                .ToTable("UserTokens", "Authentication");
        }

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
