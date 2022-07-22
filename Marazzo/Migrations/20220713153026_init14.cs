using Microsoft.EntityFrameworkCore.Migrations;

namespace Marazzo.Migrations
{
    public partial class init14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adpicture",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adpicture",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
