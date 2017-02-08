using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Athene.Inventory.Web.Data.Migrations
{
    public partial class ApplicationUserUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookItems_Students_RentedByStudentId",
                table: "BookItems");

            migrationBuilder.DropIndex(
                name: "IX_BookItems_RentedByStudentId",
                table: "BookItems");

            migrationBuilder.DropColumn(
                name: "RentedByStudentId",
                table: "BookItems");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.AddColumn<string>(
                name: "RentedByUserId",
                table: "BookItems",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthsday",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookItems_RentedByUserId",
                table: "BookItems",
                column: "RentedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookItems_AspNetUsers_RentedByUserId",
                table: "BookItems",
                column: "RentedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookItems_AspNetUsers_RentedByUserId",
                table: "BookItems");

            migrationBuilder.DropIndex(
                name: "IX_BookItems_RentedByUserId",
                table: "BookItems");

            migrationBuilder.DropColumn(
                name: "RentedByUserId",
                table: "BookItems");

            migrationBuilder.DropColumn(
                name: "Birthsday",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");

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

            migrationBuilder.AddColumn<int>(
                name: "RentedByStudentId",
                table: "BookItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookItems_RentedByStudentId",
                table: "BookItems",
                column: "RentedByStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookItems_Students_RentedByStudentId",
                table: "BookItems",
                column: "RentedByStudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
