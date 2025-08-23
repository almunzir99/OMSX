using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMSX.VendorsService.Migrations
{
    /// <inheritdoc />
    public partial class AddImageToVendorCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "VendorCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendorCategories_ImageId",
                table: "VendorCategories",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorCategories_FileInfos_ImageId",
                table: "VendorCategories",
                column: "ImageId",
                principalTable: "FileInfos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorCategories_FileInfos_ImageId",
                table: "VendorCategories");

            migrationBuilder.DropIndex(
                name: "IX_VendorCategories_ImageId",
                table: "VendorCategories");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "VendorCategories");
        }
    }
}
