using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentals.Migrations
{
    public partial class AddReturned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Returned",
                table: "RentingItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Rentings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 10, 19, 17, 6, 25, 583, DateTimeKind.Local).AddTicks(6561), new DateTime(2021, 10, 14, 17, 6, 25, 586, DateTimeKind.Local).AddTicks(4651) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Returned",
                table: "RentingItems");

            migrationBuilder.UpdateData(
                table: "Rentings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 10, 18, 20, 2, 24, 764, DateTimeKind.Local).AddTicks(4703), new DateTime(2021, 10, 13, 20, 2, 24, 767, DateTimeKind.Local).AddTicks(1315) });
        }
    }
}
