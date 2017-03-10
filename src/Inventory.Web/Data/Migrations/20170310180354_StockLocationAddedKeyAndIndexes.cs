using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Athene.Inventory.Web.Data.Migrations
{
    public partial class StockLocationAddedKeyAndIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookItems_StockLocations_StockLocationHall_StockLocationCorridor_StockLocationRack_StockLocationLevel_StockLocationPosition",
                table: "BookItems");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockLocations",
                table: "StockLocations");

            migrationBuilder.DropIndex(
                name: "IX_BookItems_StockLocationHall_StockLocationCorridor_StockLocationRack_StockLocationLevel_StockLocationPosition",
                table: "BookItems");

            migrationBuilder.DropColumn(
                name: "StockLocationCorridor",
                table: "BookItems");

            migrationBuilder.DropColumn(
                name: "StockLocationHall",
                table: "BookItems");

            migrationBuilder.DropColumn(
                name: "StockLocationLevel",
                table: "BookItems");

            migrationBuilder.DropColumn(
                name: "StockLocationPosition",
                table: "BookItems");

            migrationBuilder.RenameColumn(
                name: "StockLocationRack",
                table: "BookItems",
                newName: "StockLocationId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StockLocations",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockLocations",
                table: "StockLocations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockLocations_Hall_Corridor_Rack_Level_Position",
                table: "StockLocations",
                columns: new[] { "Hall", "Corridor", "Rack", "Level", "Position" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookItems_StockLocationId",
                table: "BookItems",
                column: "StockLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookItems_StockLocations_StockLocationId",
                table: "BookItems",
                column: "StockLocationId",
                principalTable: "StockLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookItems_StockLocations_StockLocationId",
                table: "BookItems");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockLocations",
                table: "StockLocations");

            migrationBuilder.DropIndex(
                name: "IX_StockLocations_Hall_Corridor_Rack_Level_Position",
                table: "StockLocations");

            migrationBuilder.DropIndex(
                name: "IX_BookItems_StockLocationId",
                table: "BookItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StockLocations");

            migrationBuilder.RenameColumn(
                name: "StockLocationId",
                table: "BookItems",
                newName: "StockLocationRack");

            migrationBuilder.AddColumn<int>(
                name: "StockLocationCorridor",
                table: "BookItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockLocationHall",
                table: "BookItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockLocationLevel",
                table: "BookItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockLocationPosition",
                table: "BookItems",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockLocations",
                table: "StockLocations",
                columns: new[] { "Hall", "Corridor", "Rack", "Level", "Position" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_BookItems_StockLocationHall_StockLocationCorridor_StockLocationRack_StockLocationLevel_StockLocationPosition",
                table: "BookItems",
                columns: new[] { "StockLocationHall", "StockLocationCorridor", "StockLocationRack", "StockLocationLevel", "StockLocationPosition" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookItems_StockLocations_StockLocationHall_StockLocationCorridor_StockLocationRack_StockLocationLevel_StockLocationPosition",
                table: "BookItems",
                columns: new[] { "StockLocationHall", "StockLocationCorridor", "StockLocationRack", "StockLocationLevel", "StockLocationPosition" },
                principalTable: "StockLocations",
                principalColumns: new[] { "Hall", "Corridor", "Rack", "Level", "Position" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
