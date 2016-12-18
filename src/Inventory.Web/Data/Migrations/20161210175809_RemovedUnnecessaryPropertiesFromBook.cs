using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Athene.Inventory.Web.Data.Migrations
{
    public partial class RemovedUnnecessaryPropertiesFromBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EuropeanArticleNumber",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "InternationalStandardBookNumber10",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "InternationalStandardBookNumber13",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "InternationalStandardBookNumber",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InternationalStandardBookNumber",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "EuropeanArticleNumber",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InternationalStandardBookNumber10",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InternationalStandardBookNumber13",
                table: "Books",
                nullable: true);
        }
    }
}
