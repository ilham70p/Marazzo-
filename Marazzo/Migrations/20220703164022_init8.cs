using Microsoft.EntityFrameworkCore.Migrations;

namespace Marazzo.Migrations
{
    public partial class init8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecDetails_Products_ProductId",
                table: "SpecDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecDetails_Products_ProductId",
                table: "SpecDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecDetails_Products_ProductId",
                table: "SpecDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecDetails_Products_ProductId",
                table: "SpecDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
