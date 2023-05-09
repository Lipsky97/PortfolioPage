using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio.DB.Migrations
{
    public partial class ghLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GHLink",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasGHLink",
                table: "Projects",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GHLink",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "HasGHLink",
                table: "Projects");
        }
    }
}
