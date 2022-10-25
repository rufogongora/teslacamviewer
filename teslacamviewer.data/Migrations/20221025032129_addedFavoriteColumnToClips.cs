using Microsoft.EntityFrameworkCore.Migrations;

namespace teslacamviewer.data.Migrations
{
    public partial class addedFavoriteColumnToClips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Favorite",
                table: "TeslaClips",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favorite",
                table: "TeslaClips");
        }
    }
}
