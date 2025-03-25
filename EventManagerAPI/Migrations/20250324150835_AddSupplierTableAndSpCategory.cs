using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagerAPI.Migrations
{
    public partial class AddSupplierTableAndSpCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplierCategoryName",
                table: "Suppliers");

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierCategoryId",
                table: "Suppliers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SupplierCategories",
                columns: table => new
                {
                    SupplierCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierCategoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierCategories", x => x.SupplierCategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_SupplierCategoryId",
                table: "Suppliers",
                column: "SupplierCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_SupplierCategories_SupplierCategoryId",
                table: "Suppliers",
                column: "SupplierCategoryId",
                principalTable: "SupplierCategories",
                principalColumn: "SupplierCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_SupplierCategories_SupplierCategoryId",
                table: "Suppliers");

            migrationBuilder.DropTable(
                name: "SupplierCategories");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_SupplierCategoryId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "SupplierCategoryId",
                table: "Suppliers");

            migrationBuilder.AddColumn<string>(
                name: "SupplierCategoryName",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
