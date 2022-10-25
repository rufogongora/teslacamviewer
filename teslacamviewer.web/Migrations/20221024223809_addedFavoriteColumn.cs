using Microsoft.EntityFrameworkCore.Migrations;

namespace teslacamviewer.web.Migrations
{
    public partial class addedFavoriteColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Favorite",
                table: "TeslaFolders",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favorite",
                table: "TeslaFolders");
        }
    }
}
