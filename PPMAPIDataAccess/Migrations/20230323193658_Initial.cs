using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PPMAPIDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetNumber = table.Column<int>(type: "int", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<float>(type: "real", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OwnerUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_Owners_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "Owners",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "RentalProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RentalFee = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<float>(type: "real", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OwnerUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalProperties_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalProperties_Owners_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "Owners",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_RentalProperties_Tenants_TenantUserId",
                        column: x => x.TenantUserId,
                        principalTable: "Tenants",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Costs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RentalPropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Costs_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Costs_RentalProperties_RentalPropertyId",
                        column: x => x.RentalPropertyId,
                        principalTable: "RentalProperties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Revenues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RentalPropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revenues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Revenues_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Revenues_RentalProperties_RentalPropertyId",
                        column: x => x.RentalPropertyId,
                        principalTable: "RentalProperties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ValueDecreases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RentalPropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueDecreases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValueDecreases_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ValueDecreases_RentalProperties_RentalPropertyId",
                        column: x => x.RentalPropertyId,
                        principalTable: "RentalProperties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ValueIncreases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RentalPropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueIncreases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValueIncreases_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ValueIncreases_RentalProperties_RentalPropertyId",
                        column: x => x.RentalPropertyId,
                        principalTable: "RentalProperties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Costs_PropertyId",
                table: "Costs",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_RentalPropertyId",
                table: "Costs",
                column: "RentalPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AddressId",
                table: "Properties",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OwnerUserId",
                table: "Properties",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalProperties_AddressId",
                table: "RentalProperties",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalProperties_OwnerUserId",
                table: "RentalProperties",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalProperties_TenantUserId",
                table: "RentalProperties",
                column: "TenantUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Revenues_PropertyId",
                table: "Revenues",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Revenues_RentalPropertyId",
                table: "Revenues",
                column: "RentalPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ValueDecreases_PropertyId",
                table: "ValueDecreases",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ValueDecreases_RentalPropertyId",
                table: "ValueDecreases",
                column: "RentalPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ValueIncreases_PropertyId",
                table: "ValueIncreases",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ValueIncreases_RentalPropertyId",
                table: "ValueIncreases",
                column: "RentalPropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Costs");

            migrationBuilder.DropTable(
                name: "Revenues");

            migrationBuilder.DropTable(
                name: "ValueDecreases");

            migrationBuilder.DropTable(
                name: "ValueIncreases");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "RentalProperties");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
