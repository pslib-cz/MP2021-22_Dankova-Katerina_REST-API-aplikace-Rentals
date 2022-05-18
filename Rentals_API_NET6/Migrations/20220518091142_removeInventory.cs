using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentals_API_NET6.Migrations
{
    public partial class removeInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemHistoryLogs_Items_UserInventoryId",
                table: "ItemHistoryLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemHistoryLogs_Users_UserInventoryId",
                table: "ItemHistoryLogs");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropIndex(
                name: "IX_ItemHistoryLogs_UserInventoryId",
                table: "ItemHistoryLogs");

            migrationBuilder.DropColumn(
                name: "UserInventoryId",
                table: "ItemHistoryLogs");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemHistoryLogs_Items_UserId",
                table: "ItemHistoryLogs",
                column: "UserId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemHistoryLogs_Items_UserId",
                table: "ItemHistoryLogs");

            migrationBuilder.AddColumn<int>(
                name: "UserInventoryId",
                table: "ItemHistoryLogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => new { x.ItemId, x.UserId });
                    table.ForeignKey(
                        name: "FK_InventoryItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemHistoryLogs_UserInventoryId",
                table: "ItemHistoryLogs",
                column: "UserInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_UserId",
                table: "InventoryItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemHistoryLogs_Items_UserInventoryId",
                table: "ItemHistoryLogs",
                column: "UserInventoryId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemHistoryLogs_Users_UserInventoryId",
                table: "ItemHistoryLogs",
                column: "UserInventoryId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
