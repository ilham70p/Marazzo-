using Microsoft.EntityFrameworkCore.Migrations;

namespace Marazzo.Migrations
{
    public partial class init12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubcategoryId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubcategoryId",
                table: "Products",
                column: "SubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcategories_SubcategoryId",
                table: "Products",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcategories_SubcategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubcategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubcategoryId",
                table: "Products");
        }
    }
}
