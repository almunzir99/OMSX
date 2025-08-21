using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMSX.VendorsService.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFullPath = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendorCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryNameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorCategories_Localizations_CategoryNameId",
                        column: x => x.CategoryNameId,
                        principalTable: "Localizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VendorCategories_Localizations_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "Localizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VendorCategories_VendorCategories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "VendorCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessNameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LegalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessLicense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VATNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessType = table.Column<int>(type: "int", nullable: false),
                    BusinessEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupportEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupportPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendorCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendors_BusinessAddresses_BusinessAddressId",
                        column: x => x.BusinessAddressId,
                        principalTable: "BusinessAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendors_FileInfos_LogoId",
                        column: x => x.LogoId,
                        principalTable: "FileInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendors_Localizations_BusinessNameId",
                        column: x => x.BusinessNameId,
                        principalTable: "Localizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vendors_Localizations_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "Localizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vendors_VendorCategories_VendorCategoryId",
                        column: x => x.VendorCategoryId,
                        principalTable: "VendorCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VendorCategories_CategoryNameId",
                table: "VendorCategories",
                column: "CategoryNameId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorCategories_DescriptionId",
                table: "VendorCategories",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorCategories_ParentCategoryId",
                table: "VendorCategories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_BusinessAddressId",
                table: "Vendors",
                column: "BusinessAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_BusinessNameId",
                table: "Vendors",
                column: "BusinessNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_DescriptionId",
                table: "Vendors",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_LogoId",
                table: "Vendors",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_VendorCategoryId",
                table: "Vendors",
                column: "VendorCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "BusinessAddresses");

            migrationBuilder.DropTable(
                name: "FileInfos");

            migrationBuilder.DropTable(
                name: "VendorCategories");

            migrationBuilder.DropTable(
                name: "Localizations");
        }
    }
}
