using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Athene.Inventory.Web.Data.Migrations
{
    public partial class BookItemRelationchipsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_Books_BookId",
                table: "Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publisher_PublisherId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Author",
                table: "Author");

            migrationBuilder.AddColumn<DateTime>(
                name: "RentedAt",
                table: "BookItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RentedByStudentId",
                table: "BookItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookItems_RentedByStudentId",
                table: "BookItems",
                column: "RentedByStudentId");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherId",
                table: "Books",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Author",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Books_BookId",
                table: "Author",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publisher_PublisherId",
                table: "Books",
                column: "PublisherId",
                principalTable: "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookItems_Students_RentedByStudentId",
                table: "BookItems",
                column: "RentedByStudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_Author_BookId",
                table: "Author",
                newName: "IX_Authors_BookId");

            migrationBuilder.RenameTable(
                name: "Author",
                newName: "Authors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Books_BookId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publisher_PublisherId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_BookItems_Students_RentedByStudentId",
                table: "BookItems");

            migrationBuilder.DropIndex(
                name: "IX_BookItems_RentedByStudentId",
                table: "BookItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "RentedAt",
                table: "BookItems");

            migrationBuilder.DropColumn(
                name: "RentedByStudentId",
                table: "BookItems");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherId",
                table: "Books",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Author",
                table: "Authors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Books_BookId",
                table: "Authors",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publisher_PublisherId",
                table: "Books",
                column: "PublisherId",
                principalTable: "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_Authors_BookId",
                table: "Authors",
                newName: "IX_Author_BookId");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "Author");
        }
    }
}
