using Microsoft.EntityFrameworkCore.Migrations;

namespace teslacamviewer.data.Migrations
{
    public partial class addedmissingcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FolderType",
                table: "TeslaFolders",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FolderType",
                table: "TeslaFolders");
        }
    }
}
