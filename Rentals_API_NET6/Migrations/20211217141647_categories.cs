using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentals_API_NET6.Migrations
{
    public partial class categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CategoryItems",
                columns: new[] { "CategoryId", "ItemId" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 3, 2 },
                    { 5, 3 },
                    { 5, 4 },
                    { 3, 5 },
                    { 3, 6 },
                    { 3, 7 },
                    { 3, 8 },
                    { 3, 9 },
                    { 3, 10 },
                    { 3, 11 },
                    { 3, 12 },
                    { 2, 13 },
                    { 2, 14 },
                    { 3, 15 },
                    { 3, 16 },
                    { 2, 17 },
                    { 3, 18 },
                    { 3, 19 },
                    { 3, 20 },
                    { 3, 21 },
                    { 6, 22 },
                    { 5, 23 },
                    { 5, 24 },
                    { 5, 25 },
                    { 5, 26 },
                    { 5, 27 },
                    { 5, 28 },
                    { 5, 29 },
                    { 5, 30 },
                    { 5, 31 },
                    { 5, 32 },
                    { 5, 33 },
                    { 5, 34 },
                    { 5, 35 },
                    { 5, 36 },
                    { 5, 37 },
                    { 5, 38 },
                    { 5, 39 },
                    { 5, 40 },
                    { 5, 41 },
                    { 5, 42 }
                });

            migrationBuilder.InsertData(
                table: "CategoryItems",
                columns: new[] { "CategoryId", "ItemId" },
                values: new object[,]
                {
                    { 5, 43 },
                    { 2, 44 },
                    { 3, 45 },
                    { 2, 46 },
                    { 5, 47 },
                    { 5, 48 },
                    { 5, 49 },
                    { 5, 50 },
                    { 5, 51 },
                    { 5, 52 },
                    { 5, 53 },
                    { 5, 54 },
                    { 5, 55 },
                    { 5, 56 },
                    { 5, 57 },
                    { 5, 58 },
                    { 5, 59 },
                    { 5, 60 },
                    { 6, 61 },
                    { 6, 62 },
                    { 6, 63 },
                    { 6, 64 },
                    { 6, 65 },
                    { 5, 66 },
                    { 5, 67 },
                    { 6, 68 },
                    { 2, 69 },
                    { 4, 70 },
                    { 4, 71 },
                    { 5, 72 },
                    { 6, 73 },
                    { 4, 74 },
                    { 3, 75 },
                    { 6, 76 },
                    { 6, 77 },
                    { 4, 78 },
                    { 2, 79 },
                    { 3, 80 },
                    { 4, 81 },
                    { 4, 82 },
                    { 4, 83 },
                    { 4, 84 }
                });

            migrationBuilder.InsertData(
                table: "CategoryItems",
                columns: new[] { "CategoryId", "ItemId" },
                values: new object[,]
                {
                    { 5, 85 },
                    { 5, 86 },
                    { 4, 87 },
                    { 6, 88 },
                    { 6, 89 },
                    { 6, 90 },
                    { 6, 91 },
                    { 6, 92 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 3, 9 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 3, 10 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 3, 11 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 3, 12 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 2, 13 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 2, 14 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 3, 15 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 3, 16 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 2, 17 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 3, 18 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 3, 19 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 3, 20 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 3, 21 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 6, 22 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 23 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 24 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 25 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 26 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 27 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 28 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 29 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 30 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 31 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 32 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 33 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 34 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 35 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 36 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 37 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 38 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 39 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 40 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 41 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 42 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 43 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 2, 44 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 3, 45 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 2, 46 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 47 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 48 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 49 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 50 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 51 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 52 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 53 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 54 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 55 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 56 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 57 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 58 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 59 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 60 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 6, 61 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 6, 62 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 6, 63 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 6, 64 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 6, 65 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 66 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 67 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 6, 68 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 2, 69 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 4, 70 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 4, 71 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 72 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 6, 73 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 4, 74 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 3, 75 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 6, 76 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 6, 77 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 4, 78 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 2, 79 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 3, 80 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 4, 81 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 4, 82 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 4, 83 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 4, 84 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 85 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 5, 86 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 4, 87 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 6, 88 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 6, 89 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 6, 90 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 6, 91 });

            migrationBuilder.DeleteData(
                table: "CategoryItems",
                keyColumns: new[] { "CategoryId", "ItemId" },
                keyValues: new object[] { 6, 92 });
        }
    }
}
