using Microsoft.EntityFrameworkCore.Migrations;

namespace Athene.Inventory.Data.Migrations
{
    public partial class AddIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Matchcode",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RentedByUserId",
                table: "InventoryItems",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExternalId",
                table: "InventoryItems",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "InventoryItems",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matchcode_Value",
                table: "Matchcode",
                column: "Value");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_Barcode",
                table: "InventoryItems",
                column: "Barcode");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ExternalId",
                table: "InventoryItems",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_RentedByUserId",
                table: "InventoryItems",
                column: "RentedByUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matchcode_Value",
                table: "Matchcode");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItems_Barcode",
                table: "InventoryItems");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItems_ExternalId",
                table: "InventoryItems");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItems_RentedByUserId",
                table: "InventoryItems");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Matchcode",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RentedByUserId",
                table: "InventoryItems",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExternalId",
                table: "InventoryItems",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "InventoryItems",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
