﻿using Microsoft.EntityFrameworkCore;
using PPMAPIModelLibrary.FinancialObjects.Transactions;
using PPMAPIModelLibrary.FinancialObjects.ValueModifiers;
using PPMAPIModelLibrary.Properties;
using PPMAPIModelLibrary.Users;
using PPMAPIModelLibrary.UtilityModels;

namespace PPMAPIDataAccess
{
    public class PPMDbContext : DbContext
    {
        // Create Dbsets for models
        public DbSet<Property> Properties { get; set; }
        public DbSet<RentalProperty> RentalProperties { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<Revenue> Revenues { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ValueIncrease> ValueIncreases { get; set; }
        public DbSet<ValueDecrease> ValueDecreases{ get; set; }


        // for checking that DI is getting a different instance each time when the dbcontext is injected in the context of a web request
        private Guid _instanceId = Guid.NewGuid();

        public PPMDbContext(DbContextOptions<PPMDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Property>()
                .Property(p => p.PurchasePrice)
                .HasColumnType("decimal");

            builder.Entity<RentalProperty>()
                .Property(r => r.RentalFee)
                .HasColumnType("decimal");

            builder.Entity<RentalProperty>()
                .Property(r => r.PurchasePrice)
                .HasColumnType("decimal");

            builder.Entity<Cost>()
                .Property(c => c.Value)
                .HasColumnType("decimal");

            builder.Entity<Revenue>()
                .Property(c => c.Value)
                .HasColumnType("decimal");

            builder.Entity<ValueIncrease>()
                .Property(c => c.Value)
                .HasColumnType("decimal");

            builder.Entity<ValueDecrease>()
                .Property(c => c.Value)
                .HasColumnType("decimal");

            base.OnModelCreating(builder);
        }
        public static void AddBaseOptions(DbContextOptionsBuilder<PPMDbContext> builder, string connectionString)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string must be provided", nameof(connectionString));

            builder.UseSqlServer(connectionString, x => { x.EnableRetryOnFailure(); });
        }
    }
}
