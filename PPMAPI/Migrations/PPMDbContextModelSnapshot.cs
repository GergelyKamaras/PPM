// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PPMAPI.DataAccess;

#nullable disable

namespace PPMAPI.Migrations
{
    [DbContext(typeof(PPMDbContext))]
    partial class PPMDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PPMModelLibrary.Models.Properties.Property", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("decimal");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("OwnerUserId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("PPMModelLibrary.Models.Properties.RentableProperty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("decimal");

                    b.Property<decimal>("RentalFee")
                        .HasColumnType("decimal");

                    b.Property<string>("TenantUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("OwnerUserId");

                    b.HasIndex("TenantUserId");

                    b.ToTable("RentableProperties");
                });

            modelBuilder.Entity("PPMModelLibrary.Models.Transactions.Cost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RentablePropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.HasIndex("RentablePropertyId");

                    b.ToTable("Costs");
                });

            modelBuilder.Entity("PPMModelLibrary.Models.Transactions.Revenue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RentablePropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.HasIndex("RentablePropertyId");

                    b.ToTable("Revenues");
                });

            modelBuilder.Entity("PPMModelLibrary.Models.Users.Owner", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("PPMModelLibrary.Models.Users.Tenant", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("PPMModelLibrary.Models.UtilityModels.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdditionalInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("PPMModelLibrary.Models.ValueModifiers.ValueDecrease", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RentablePropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.HasIndex("RentablePropertyId");

                    b.ToTable("ValueDecreases");
                });

            modelBuilder.Entity("PPMModelLibrary.Models.ValueModifiers.ValueIncrease", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RentablePropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.HasIndex("RentablePropertyId");

                    b.ToTable("ValueIncreases");
                });

            modelBuilder.Entity("PPMModelLibrary.Models.Properties.Property", b =>
                {
                    b.HasOne("PPMModelLibrary.Models.UtilityModels.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PPMModelLibrary.Models.Users.Owner", null)
                        .WithMany("Properties")
                        .HasForeignKey("OwnerUserId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("PPMModelLibrary.Models.Properties.RentableProperty", b =>
                {
                    b.HasOne("PPMModelLibrary.Models.UtilityModels.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PPMModelLibrary.Models.Users.Owner", null)
                        .WithMany("Rentableproperties")
                        .HasForeignKey("OwnerUserId");

                    b.HasOne("PPMModelLibrary.Models.Users.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("PPMModelLibrary.Models.Transactions.Cost", b =>
                {
                    b.HasOne("PPMModelLibrary.Models.Properties.Property", null)
                        .WithMany("Costs")
                        .HasForeignKey("PropertyId");

                    b.HasOne("PPMModelLibrary.Models.Properties.RentableProperty", null)
                        .WithMany("Costs")
                        .HasForeignKey("RentablePropertyId");
                });

            modelBuilder.Entity("PPMModelLibrary.Models.Transactions.Revenue", b =>
                {
                    b.HasOne("PPMModelLibrary.Models.Properties.Property", null)
                        .WithMany("Revenues")
                        .HasForeignKey("PropertyId");

                    b.HasOne("PPMModelLibrary.Models.Properties.RentableProperty", null)
                        .WithMany("Revenues")
                        .HasForeignKey("RentablePropertyId");
                });

            modelBuilder.Entity("PPMModelLibrary.Models.ValueModifiers.ValueDecrease", b =>
                {
                    b.HasOne("PPMModelLibrary.Models.Properties.Property", null)
                        .WithMany("ValueDecreases")
                        .HasForeignKey("PropertyId");

                    b.HasOne("PPMModelLibrary.Models.Properties.RentableProperty", null)
                        .WithMany("ValueDecreases")
                        .HasForeignKey("RentablePropertyId");
                });

            modelBuilder.Entity("PPMModelLibrary.Models.ValueModifiers.ValueIncrease", b =>
                {
                    b.HasOne("PPMModelLibrary.Models.Properties.Property", null)
                        .WithMany("ValueIncreases")
                        .HasForeignKey("PropertyId");

                    b.HasOne("PPMModelLibrary.Models.Properties.RentableProperty", null)
                        .WithMany("ValueIncreases")
                        .HasForeignKey("RentablePropertyId");
                });

            modelBuilder.Entity("PPMModelLibrary.Models.Properties.Property", b =>
                {
                    b.Navigation("Costs");

                    b.Navigation("Revenues");

                    b.Navigation("ValueDecreases");

                    b.Navigation("ValueIncreases");
                });

            modelBuilder.Entity("PPMModelLibrary.Models.Properties.RentableProperty", b =>
                {
                    b.Navigation("Costs");

                    b.Navigation("Revenues");

                    b.Navigation("ValueDecreases");

                    b.Navigation("ValueIncreases");
                });

            modelBuilder.Entity("PPMModelLibrary.Models.Users.Owner", b =>
                {
                    b.Navigation("Properties");

                    b.Navigation("Rentableproperties");
                });
#pragma warning restore 612, 618
        }
    }
}
