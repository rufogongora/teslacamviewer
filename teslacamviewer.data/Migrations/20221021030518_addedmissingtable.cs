using Microsoft.EntityFrameworkCore.Migrations;

namespace teslacamviewer.data.Migrations
{
    public partial class addedmissingtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeslaClips_TeslaClipsGroup_TeslaClipsGroupId",
                table: "TeslaClips");

            migrationBuilder.DropForeignKey(
                name: "FK_TeslaClipsGroup_TeslaFolders_TeslaFolderId",
                table: "TeslaClipsGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeslaClipsGroup",
                table: "TeslaClipsGroup");

            migrationBuilder.RenameTable(
                name: "TeslaClipsGroup",
                newName: "TeslaClipsGroups");

            migrationBuilder.RenameIndex(
                name: "IX_TeslaClipsGroup_TeslaFolderId",
                table: "TeslaClipsGroups",
                newName: "IX_TeslaClipsGroups_TeslaFolderId");

            migrationBuilder.AddColumn<int>(
                name: "TeslaClipGroupId",
                table: "TeslaClips",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeslaClipsGroups",
                table: "TeslaClipsGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeslaClips_TeslaClipsGroups_TeslaClipsGroupId",
                table: "TeslaClips",
                column: "TeslaClipsGroupId",
                principalTable: "TeslaClipsGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeslaClipsGroups_TeslaFolders_TeslaFolderId",
                table: "TeslaClipsGroups",
                column: "TeslaFolderId",
                principalTable: "TeslaFolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeslaClips_TeslaClipsGroups_TeslaClipsGroupId",
                table: "TeslaClips");

            migrationBuilder.DropForeignKey(
                name: "FK_TeslaClipsGroups_TeslaFolders_TeslaFolderId",
                table: "TeslaClipsGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeslaClipsGroups",
                table: "TeslaClipsGroups");

            migrationBuilder.DropColumn(
                name: "TeslaClipGroupId",
                table: "TeslaClips");

            migrationBuilder.RenameTable(
                name: "TeslaClipsGroups",
                newName: "TeslaClipsGroup");

            migrationBuilder.RenameIndex(
                name: "IX_TeslaClipsGroups_TeslaFolderId",
                table: "TeslaClipsGroup",
                newName: "IX_TeslaClipsGroup_TeslaFolderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeslaClipsGroup",
                table: "TeslaClipsGroup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeslaClips_TeslaClipsGroup_TeslaClipsGroupId",
                table: "TeslaClips",
                column: "TeslaClipsGroupId",
                principalTable: "TeslaClipsGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeslaClipsGroup_TeslaFolders_TeslaFolderId",
                table: "TeslaClipsGroup",
                column: "TeslaFolderId",
                principalTable: "TeslaFolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
