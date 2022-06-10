using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Translator.Persistence.Migrations
{
    public partial class logs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeetSpeakTranslations_AspNetUsers_CreatedById",
                table: "LeetSpeakTranslations");

            migrationBuilder.DropIndex(
                name: "IX_LeetSpeakTranslations_CreatedById",
                table: "LeetSpeakTranslations");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "LeetSpeakTranslations");

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logger = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Callsite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "LeetSpeakTranslations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeetSpeakTranslations_CreatedById",
                table: "LeetSpeakTranslations",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_LeetSpeakTranslations_AspNetUsers_CreatedById",
                table: "LeetSpeakTranslations",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
