using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentals_API_NET6.Migrations
{
    public partial class Accesories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AccessoryItems",
                columns: new[] { "AccessoryId", "ItemId" },
                values: new object[,]
                {
                    { 13, 11 },
                    { 14, 11 },
                    { 17, 11 },
                    { 24, 11 },
                    { 25, 11 },
                    { 46, 11 },
                    { 48, 11 },
                    { 49, 11 },
                    { 50, 11 },
                    { 51, 11 },
                    { 52, 11 },
                    { 63, 11 },
                    { 64, 11 },
                    { 69, 11 },
                    { 13, 12 },
                    { 14, 12 },
                    { 17, 12 },
                    { 24, 12 },
                    { 25, 12 },
                    { 46, 12 },
                    { 48, 12 },
                    { 49, 12 },
                    { 50, 12 },
                    { 51, 12 },
                    { 52, 12 },
                    { 63, 12 },
                    { 64, 12 },
                    { 69, 12 },
                    { 13, 15 },
                    { 14, 15 },
                    { 17, 15 },
                    { 22, 15 },
                    { 44, 15 },
                    { 46, 15 },
                    { 48, 15 },
                    { 49, 15 },
                    { 53, 15 },
                    { 54, 15 },
                    { 55, 15 },
                    { 61, 15 },
                    { 62, 15 },
                    { 69, 15 }
                });

            migrationBuilder.InsertData(
                table: "AccessoryItems",
                columns: new[] { "AccessoryId", "ItemId" },
                values: new object[,]
                {
                    { 72, 15 },
                    { 13, 16 },
                    { 14, 16 },
                    { 17, 16 },
                    { 22, 16 },
                    { 44, 16 },
                    { 46, 16 },
                    { 48, 16 },
                    { 49, 16 },
                    { 53, 16 },
                    { 54, 16 },
                    { 55, 16 },
                    { 61, 16 },
                    { 62, 16 },
                    { 69, 16 },
                    { 72, 16 },
                    { 47, 18 },
                    { 57, 18 },
                    { 58, 18 },
                    { 59, 18 },
                    { 47, 19 },
                    { 57, 19 },
                    { 58, 19 },
                    { 59, 19 },
                    { 47, 20 },
                    { 57, 20 },
                    { 58, 20 },
                    { 59, 20 },
                    { 47, 21 },
                    { 57, 21 },
                    { 58, 21 },
                    { 59, 21 },
                    { 40, 24 },
                    { 41, 24 },
                    { 42, 24 },
                    { 43, 24 },
                    { 40, 25 },
                    { 41, 25 },
                    { 42, 25 },
                    { 43, 25 },
                    { 40, 38 },
                    { 41, 38 }
                });

            migrationBuilder.InsertData(
                table: "AccessoryItems",
                columns: new[] { "AccessoryId", "ItemId" },
                values: new object[,]
                {
                    { 42, 38 },
                    { 43, 38 },
                    { 40, 39 },
                    { 41, 39 },
                    { 42, 39 },
                    { 43, 39 },
                    { 46, 45 },
                    { 48, 45 },
                    { 49, 45 },
                    { 53, 61 },
                    { 54, 61 },
                    { 55, 61 },
                    { 53, 62 },
                    { 54, 62 },
                    { 55, 62 },
                    { 50, 63 },
                    { 51, 63 },
                    { 52, 63 },
                    { 50, 64 },
                    { 51, 64 },
                    { 52, 64 },
                    { 26, 65 },
                    { 27, 65 },
                    { 28, 65 },
                    { 29, 65 },
                    { 30, 65 },
                    { 31, 65 },
                    { 32, 65 },
                    { 33, 65 },
                    { 34, 65 },
                    { 35, 65 },
                    { 36, 65 },
                    { 37, 65 },
                    { 78, 76 },
                    { 78, 77 },
                    { 60, 80 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "Img", "IsDeleted", "Name", "Note", "State" },
                values: new object[,]
                {
                    { 93, null, null, true, "Alkalická baterie GP 2700 1.2 V AA", null, 0 },
                    { 94, null, null, true, "Alkalická baterie GP 2700 1.2 V AA", null, 0 },
                    { 95, null, null, true, "Alkalická baterie GP 2700 1.2 V AA", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "AccessoryItems",
                columns: new[] { "AccessoryId", "ItemId" },
                values: new object[] { 93, 65 });

            migrationBuilder.InsertData(
                table: "AccessoryItems",
                columns: new[] { "AccessoryId", "ItemId" },
                values: new object[] { 94, 65 });

            migrationBuilder.InsertData(
                table: "AccessoryItems",
                columns: new[] { "AccessoryId", "ItemId" },
                values: new object[] { 95, 65 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 13, 11 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 14, 11 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 17, 11 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 24, 11 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 25, 11 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 46, 11 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 48, 11 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 49, 11 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 50, 11 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 51, 11 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 52, 11 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 63, 11 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 64, 11 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 69, 11 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 13, 12 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 14, 12 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 17, 12 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 24, 12 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 25, 12 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 46, 12 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 48, 12 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 49, 12 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 50, 12 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 51, 12 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 52, 12 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 63, 12 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 64, 12 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 69, 12 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 13, 15 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 14, 15 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 17, 15 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 22, 15 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 44, 15 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 46, 15 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 48, 15 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 49, 15 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 53, 15 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 54, 15 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 55, 15 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 61, 15 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 62, 15 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 69, 15 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 72, 15 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 13, 16 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 14, 16 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 17, 16 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 22, 16 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 44, 16 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 46, 16 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 48, 16 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 49, 16 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 53, 16 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 54, 16 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 55, 16 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 61, 16 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 62, 16 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 69, 16 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 72, 16 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 47, 18 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 57, 18 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 58, 18 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 59, 18 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 47, 19 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 57, 19 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 58, 19 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 59, 19 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 47, 20 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 57, 20 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 58, 20 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 59, 20 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 47, 21 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 57, 21 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 58, 21 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 59, 21 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 40, 24 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 41, 24 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 42, 24 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 43, 24 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 40, 25 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 41, 25 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 42, 25 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 43, 25 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 40, 38 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 41, 38 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 42, 38 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 43, 38 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 40, 39 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 41, 39 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 42, 39 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 43, 39 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 46, 45 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 48, 45 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 49, 45 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 53, 61 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 54, 61 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 55, 61 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 53, 62 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 54, 62 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 55, 62 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 50, 63 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 51, 63 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 52, 63 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 50, 64 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 51, 64 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 52, 64 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 26, 65 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 27, 65 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 28, 65 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 29, 65 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 30, 65 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 31, 65 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 32, 65 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 33, 65 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 34, 65 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 35, 65 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 36, 65 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 37, 65 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 93, 65 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 94, 65 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 95, 65 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 78, 76 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 78, 77 });

            migrationBuilder.DeleteData(
                table: "AccessoryItems",
                keyColumns: new[] { "AccessoryId", "ItemId" },
                keyValues: new object[] { 60, 80 });

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 95);
        }
    }
}
