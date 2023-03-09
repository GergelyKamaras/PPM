using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PPMAPI.Migrations
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
                    Number = table.Column<int>(type: "int", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
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
                name: "RentableProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RentalFee = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    OwnerUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentableProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentableProperties_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentableProperties_Owners_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "Owners",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_RentableProperties_Tenants_TenantUserId",
                        column: x => x.TenantUserId,
                        principalTable: "Tenants",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Costs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: true),
                    RentablePropertyId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_Costs_RentableProperties_RentablePropertyId",
                        column: x => x.RentablePropertyId,
                        principalTable: "RentableProperties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Revenues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: true),
                    RentablePropertyId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_Revenues_RentableProperties_RentablePropertyId",
                        column: x => x.RentablePropertyId,
                        principalTable: "RentableProperties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ValueDecreases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: true),
                    RentablePropertyId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_ValueDecreases_RentableProperties_RentablePropertyId",
                        column: x => x.RentablePropertyId,
                        principalTable: "RentableProperties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ValueIncreases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: true),
                    RentablePropertyId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_ValueIncreases_RentableProperties_RentablePropertyId",
                        column: x => x.RentablePropertyId,
                        principalTable: "RentableProperties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Costs_PropertyId",
                table: "Costs",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_RentablePropertyId",
                table: "Costs",
                column: "RentablePropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AddressId",
                table: "Properties",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OwnerUserId",
                table: "Properties",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RentableProperties_AddressId",
                table: "RentableProperties",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_RentableProperties_OwnerUserId",
                table: "RentableProperties",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RentableProperties_TenantUserId",
                table: "RentableProperties",
                column: "TenantUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Revenues_PropertyId",
                table: "Revenues",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Revenues_RentablePropertyId",
                table: "Revenues",
                column: "RentablePropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ValueDecreases_PropertyId",
                table: "ValueDecreases",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ValueDecreases_RentablePropertyId",
                table: "ValueDecreases",
                column: "RentablePropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ValueIncreases_PropertyId",
                table: "ValueIncreases",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ValueIncreases_RentablePropertyId",
                table: "ValueIncreases",
                column: "RentablePropertyId");
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
                name: "RentableProperties");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
