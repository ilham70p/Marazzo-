using Microsoft.EntityFrameworkCore.Migrations;

namespace Marazzo.Migrations
{
    public partial class init11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecDetails_Specs_SpecId",
                table: "SpecDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecDetails_Specs_SpecId",
                table: "SpecDetails",
                column: "SpecId",
                principalTable: "Specs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecDetails_Specs_SpecId",
                table: "SpecDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecDetails_Specs_SpecId",
                table: "SpecDetails",
                column: "SpecId",
                principalTable: "Specs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
