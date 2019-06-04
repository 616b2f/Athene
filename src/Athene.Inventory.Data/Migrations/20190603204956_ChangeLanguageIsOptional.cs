using Microsoft.EntityFrameworkCore.Migrations;

namespace Athene.Inventory.Data.Migrations
{
    public partial class ChangeLanguageIsOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Languages_LanguageId",
                table: "Articles");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Languages_LanguageId",
                table: "Articles",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Languages_LanguageId",
                table: "Articles");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Languages_LanguageId",
                table: "Articles",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
