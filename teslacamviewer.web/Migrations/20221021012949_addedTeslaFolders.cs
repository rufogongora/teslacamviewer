using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teslacamviewer.web.Migrations
{
    public partial class addedTeslaFolders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeslaEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Est_Lat = table.Column<double>(type: "REAL", nullable: false),
                    Est_Lon = table.Column<double>(type: "REAL", nullable: false),
                    Reason = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeslaEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeslaFolders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ActualPath = table.Column<string>(type: "TEXT", nullable: true),
                    Thumbnail = table.Column<bool>(type: "INTEGER", nullable: false),
                    TeslaEventId = table.Column<int>(type: "INTEGER", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    HardDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeslaFolders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeslaFolders_TeslaEvents_TeslaEventId",
                        column: x => x.TeslaEventId,
                        principalTable: "TeslaEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeslaClips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ActualPath = table.Column<string>(type: "TEXT", nullable: true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Side = table.Column<int>(type: "INTEGER", nullable: false),
                    TeslaFolderId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeslaClips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeslaClips_TeslaFolders_TeslaFolderId",
                        column: x => x.TeslaFolderId,
                        principalTable: "TeslaFolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeslaClips_TeslaFolderId",
                table: "TeslaClips",
                column: "TeslaFolderId");

            migrationBuilder.CreateIndex(
                name: "IX_TeslaFolders_TeslaEventId",
                table: "TeslaFolders",
                column: "TeslaEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeslaClips");

            migrationBuilder.DropTable(
                name: "TeslaFolders");

            migrationBuilder.DropTable(
                name: "TeslaEvents");
        }
    }
}
