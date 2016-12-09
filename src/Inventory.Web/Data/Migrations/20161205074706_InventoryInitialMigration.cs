using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Athene.Inventory.Web.Data.Migrations
{
    public partial class InventoryInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockLocations",
                columns: table => new
                {
                    Hall = table.Column<int>(nullable: false),
                    Corridor = table.Column<int>(nullable: false),
                    Rack = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Position = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockLocations", x => new { x.Hall, x.Corridor, x.Rack, x.Level, x.Position });
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Birthsday = table.Column<DateTime>(nullable: false),
                    Lastname = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Description = table.Column<string>(nullable: true),
                    EuropeanArticleNumber = table.Column<string>(nullable: true),
                    InternationalStandardBookNumber10 = table.Column<string>(nullable: true),
                    InternationalStandardBookNumber13 = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false),
                    PublisherId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    BookId = table.Column<int>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Author_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    BookId = table.Column<int>(nullable: true),
                    StockLocationCorridor = table.Column<int>(nullable: true),
                    StockLocationHall = table.Column<int>(nullable: true),
                    StockLocationLevel = table.Column<int>(nullable: true),
                    StockLocationPosition = table.Column<int>(nullable: true),
                    StockLocationRack = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookItems_StockLocations_StockLocationHall_StockLocationCorridor_StockLocationRack_StockLocationLevel_StockLocationPosition",
                        columns: x => new { x.StockLocationHall, x.StockLocationCorridor, x.StockLocationRack, x.StockLocationLevel, x.StockLocationPosition },
                        principalTable: "StockLocations",
                        principalColumns: new[] { "Hall", "Corridor", "Rack", "Level", "Position" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookItemNote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    BookItemId = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookItemNote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookItemNote_BookItems_BookItemId",
                        column: x => x.BookItemId,
                        principalTable: "BookItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Author_BookId",
                table: "Author",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_LanguageId",
                table: "Books",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_BookItems_BookId",
                table: "BookItems",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookItems_StockLocationHall_StockLocationCorridor_StockLocationRack_StockLocationLevel_StockLocationPosition",
                table: "BookItems",
                columns: new[] { "StockLocationHall", "StockLocationCorridor", "StockLocationRack", "StockLocationLevel", "StockLocationPosition" });

            migrationBuilder.CreateIndex(
                name: "IX_BookItemNote_BookItemId",
                table: "BookItemNote",
                column: "BookItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "BookItemNote");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "BookItems");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "StockLocations");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Publisher");
        }
    }
}
