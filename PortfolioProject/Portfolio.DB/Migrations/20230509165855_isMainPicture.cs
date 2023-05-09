using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio.DB.Migrations
{
    public partial class isMainPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMainPicture",
                table: "PortfolioPictures",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMainPicture",
                table: "PortfolioPictures");
        }
    }
}
