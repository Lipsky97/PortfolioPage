using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio.DB.Migrations
{
    public partial class setPictureVisible : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "PortfolioPictures",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "PortfolioPictures");
        }
    }
}
