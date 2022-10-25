using Microsoft.EntityFrameworkCore.Migrations;

namespace teslacamviewer.data.Migrations
{
    public partial class AddedPlaceHolderColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlaceHolderColumn",
                table: "TeslaDatas",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceHolderColumn",
                table: "TeslaDatas");
        }
    }
}
