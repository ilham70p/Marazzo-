using Microsoft.EntityFrameworkCore.Migrations;

namespace Marazzo.Migrations
{
    public partial class init15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Link",
                table: "Images",
                newName: "ImageName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Images",
                newName: "Link");
        }
    }
}
