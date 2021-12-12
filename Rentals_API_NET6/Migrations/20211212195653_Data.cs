using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentals_API_NET6.Migrations
{
    public partial class Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RentingItems",
                keyColumns: new[] { "ItemId", "RentingId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Rentings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Těla fotoaparátů");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Objektivy");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "Fotoaparáty" },
                    { 4, "Stativy" },
                    { 5, "Příslušenství" },
                    { 6, "Ostatní" }
                });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name", "Note", "State" },
                values: new object[] { "Kamera", "Kamera", null, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "IsDeleted", "Name", "Note", "State" },
                values: new object[] { "Kamera", true, "Kamera", null, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "IsDeleted", "Name", "Note" },
                values: new object[] { "Dobrý zvuk", true, "Mikrofon", null });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "Img", "IsDeleted", "Name", "Note", "State" },
                values: new object[,]
                {
                    { 4, "Dobrý zvuk", null, true, "Mikrofon", null, 0 },
                    { 5, "24,1 Mpx", null, true, "Kamera Sony", null, 0 },
                    { 6, "24,1 Mpx", null, true, "Kamera Sony", null, 0 },
                    { 7, "24,1 Mpx", null, true, "Kamera Sony", null, 0 },
                    { 8, "Lehké tělo, kompaktní, s možností až 4K videa", null, true, "Zrcadlovka SONY Alpha A6300", null, 0 },
                    { 9, "Lehké tělo, kompaktní, s možností až 4K videa", null, true, "Zrcadlovka SONY Alpha A6300", null, 0 },
                    { 10, "Lehké tělo, kompaktní, s možností až 4K videa", null, true, "Zrcadlovka SONY Alpha A6300", null, 0 },
                    { 11, null, null, false, "Fotoaparát Canon EOS 650D", "Bez očnice", 0 },
                    { 12, null, null, false, "Fotoaparát Canon EOS 650D", "Bez očnice", 0 },
                    { 13, null, null, false, "Objektiv SIGMA 17-50 mm 1:2.8", "Prstenec transfokátoru má vůli", 0 },
                    { 14, null, null, false, "Objektiv SIGMA 17-50 mm 1:2.8", "Určen primárně do ateliéru!", 0 },
                    { 15, null, null, false, "Fotoaparát Canon EOS 70D", null, 0 },
                    { 16, null, null, false, "Fotoaparát Canon EOS 70D", "Určen primárně do ateliéru!", 0 },
                    { 17, "FullFrame objektiv", null, false, "Objektiv SIGMA 24-70 mm 1:2.8", null, 0 },
                    { 18, "Stříbrná", null, false, "Videokamera Sony 1.9/2.1-57", null, 0 },
                    { 19, "Stříbrná", null, false, "Videokamera Sony 1.9/2.1-57", null, 0 },
                    { 20, "Stříbrná", null, false, "Videokamera Sony 1.9/2.1-57", null, 0 },
                    { 21, "F1.8/f1.9-57 (černá)", null, false, "Videokamera Sony HDR-CX320", null, 0 },
                    { 22, null, null, false, "Bateriový grip Phottix BG 70D", null, 0 },
                    { 23, null, null, true, "Knoflíková baterie GP Alkaline A76 LR44 V13GA 1.5 V", null, 0 },
                    { 24, "90 MB/s", null, false, "SD karta SanDisk 64 GB", null, 0 },
                    { 25, "90 MB/s", null, false, "SD karta SanDisk 64 GB", null, 0 },
                    { 26, null, null, true, "Alkalická baterie Alkalisk 1.5 V LR03 AAA Ikea", null, 0 },
                    { 27, null, null, true, "Alkalická baterie Alkalisk 1.5 V LR03 AAA Ikea", null, 0 },
                    { 28, null, null, true, "Alkalická baterie Eneloop 1.2 V HR 3UTG AA", null, 0 },
                    { 29, null, null, true, "Alkalická baterie Eneloop 1.2 V HR 4UTG AAA", null, 0 },
                    { 30, null, null, true, "Alkalická baterie LSD 1.2 V AAA", null, 0 },
                    { 31, null, null, true, "Alkalická baterie LSD 1.2 V AAA", null, 0 },
                    { 32, null, null, true, "Alkalická baterie LSD 1.2 V AAA", null, 0 },
                    { 33, null, null, true, "Alkalická baterie LSD 1.2 V AA", null, 0 },
                    { 34, null, null, true, "Alkalická baterie LSD 1.2 V AA", null, 0 },
                    { 35, null, null, true, "Alkalická baterie LSD 1.2 V AA", null, 0 },
                    { 36, null, null, true, "Alkalická baterie LSD 1.2 V AA", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "Img", "IsDeleted", "Name", "Note", "State" },
                values: new object[,]
                {
                    { 37, null, null, true, "Alkalická baterie LSD 1.2 V AA", null, 0 },
                    { 38, "Černý", null, true, "MicroSD karta adapter SAMSUNG", null, 0 },
                    { 39, "Černý", null, false, "SD karta Kingston SD10G3 32 GB", null, 0 },
                    { 40, "Průhledné", null, true, "Pouzdro na SD kartu", null, 0 },
                    { 41, "Průhledné", null, true, "Pouzdro na SD kartu", null, 0 },
                    { 42, "Průhledné", null, true, "Pouzdro na SD kartu", null, 0 },
                    { 43, "Průhledné", null, true, "Pouzdro na SD kartu", null, 0 },
                    { 44, "FullFrame", null, false, "Objektiv Canon ULTRASONIC 70-200 mm F4", null, 0 },
                    { 45, "Černý", null, false, "Fotoaparát Canon EOS 350D", null, 0 },
                    { 46, "FullFrame", null, false, "Objektiv Canon 50 mm F1.8", null, 0 },
                    { 47, "Černá", null, false, "Baterie SONY NP FV70A 1900mAh", null, 0 },
                    { 48, "Černá", null, true, "Baterie Canon LP E6N 1865mAh", null, 0 },
                    { 49, "Černá", null, true, "Baterie Canon LP E6 1800mAh", null, 0 },
                    { 50, "k 650D (šedá)", null, false, "Baterie Canon LP E8 1120mAh", null, 0 },
                    { 51, "k 650D (šedá)", null, false, "Baterie Canon LP E8 1120mAh", null, 0 },
                    { 52, "k 650D (šedá)", null, false, "Baterie Canon LP E8 1120mAh", null, 0 },
                    { 53, "k C70D, C5D", null, false, "Baterie Canon LP E6 1800mAh", null, 0 },
                    { 54, "k C70D, C5D", null, false, "Baterie Canon LP E6 1800mAh", null, 0 },
                    { 55, "k C70D, C5D", null, false, "Baterie Canon LP E6 1800mAh", null, 0 },
                    { 56, "Černá", null, true, "Baterie Canon LP E6 1865mAh", null, 0 },
                    { 57, "Černá", null, false, "Baterie SONY NP FV30 500mAh", null, 0 },
                    { 58, "Černá", null, false, "Baterie SONY NP FV30 500mAh", null, 0 },
                    { 59, "Černá", null, false, "Baterie SONY NP FV30 500mAh", null, 0 },
                    { 60, "náhradní akumulátor", null, false, "Baterie Panasonic VW VBN130 1250mAh", null, 0 },
                    { 61, "k C70D, C5D", null, false, "Nabíječka baterií Canon LC E6E", null, 0 },
                    { 62, "k C70D, C5D", null, false, "Nabíječka baterií Canon LC E6E", null, 0 },
                    { 63, "k C650D", null, false, "Nabíječka baterií Canon LC E8E", null, 0 },
                    { 64, "k C650D", null, false, "Nabíječka baterií Canon LC E8E", null, 0 },
                    { 65, "Černá", null, false, "Nabíječka baterií FK technics BC 450", null, 0 },
                    { 66, "Černý", null, true, "Napájecí kabel I SHENG 033", null, 0 },
                    { 67, "Černý", null, true, "Napájecí kabel I SHENG 033", null, 0 },
                    { 68, null, null, true, "Testujeme s.r.o.", null, 0 },
                    { 69, "(Kz)", null, false, "Objektiv SIGMA 18-200 mm 1:3.5-6.3", null, 0 },
                    { 70, null, null, false, "Stativ Velbon C-600", "xxx 2308", 0 },
                    { 71, null, null, false, "Stativ Velbon C-600", null, 0 },
                    { 72, "95 MB/s", null, false, "SD karta Lexar 64GB", null, 0 },
                    { 73, "Rezervace místnosti se studiovými světly a odpalovačem.", null, false, "Ateliér B210", null, 0 },
                    { 74, "Hlava má vůli; nevhodný na video", null, false, "Stativ Hama Star 62", null, 0 },
                    { 75, "Steadicam Camtree Wonder-3", null, false, "Flycam HD-3000", null, 0 },
                    { 76, null, null, false, "Rekordér Zoom H1", null, 0 },
                    { 77, null, null, false, "Rekordér Zoom H1", null, 0 },
                    { 78, null, null, false, "stativ MiniTripod plochý", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "Img", "IsDeleted", "Name", "Note", "State" },
                values: new object[,]
                {
                    { 79, null, null, false, "Objektiv Sigma 30mm/F1.4", null, 0 },
                    { 80, "F1,5 f2,84-34,1mm (včetně akumulátoru)", null, false, "Videokamera Panasonic HC-X920", null, 0 },
                    { 81, null, null, false, "Stativ Rig Spider FT-10", null, 0 },
                    { 82, null, null, false, "Stativ Rig Spider FT-10", null, 0 },
                    { 83, null, null, false, "Stativ Rig Spider FT-10", null, 0 },
                    { 84, "(kulová hlava)", null, false, "Stativ video MS-007H", "poškozen zip", 0 },
                    { 85, "bez předzesilovače, induktivní", null, false, "Mikrofon klopový", null, 0 },
                    { 86, "bez předzesilovače, induktivní", null, false, "Mikrofon klopový", null, 0 },
                    { 87, null, null, false, "Stativ Sony VCT-D680RM", null, 0 },
                    { 88, "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché až primitivní konstrukci nabývá minimalistického a elegantního vzhledu. ", null, true, "zkušební předmět", "první předmět", 0 },
                    { 89, "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché až primitivní konstrukci nabývá minimalistického a elegantního vzhledu. ", null, true, "zkušební předmět", "druhý předmět", 0 },
                    { 90, "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché až primitivní konstrukci nabývá minimalistického a elegantního vzhledu. ", null, true, "zkušební předmět", null, 0 },
                    { 91, "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché konstrukci nabývá minimalistického a elegantního vzhledu. ", null, true, "Fiktivní předmět", null, 0 },
                    { 92, "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché konstrukci nabývá minimalistického a elegantního vzhledu. ", null, true, "Fiktivní předmět", null, 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Fotoaparáty");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Kamery");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name", "Note", "State" },
                values: new object[] { "Popis", "Jméno", "Poznámka", 2 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "IsDeleted", "Name", "Note", "State" },
                values: new object[] { "Popis", false, "Jméno2", "Poznámka", 1 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "IsDeleted", "Name", "Note" },
                values: new object[] { "Popis", false, "Jméno3", "Poznámka" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "OauthId", "Trustfulness", "Username" },
                values: new object[,]
                {
                    { 1, "Admin", null, null, 0, null },
                    { 2, "Jan", "Novák", null, 0, null }
                });

            migrationBuilder.InsertData(
                table: "Rentings",
                columns: new[] { "Id", "ApproverId", "End", "Note", "OwnerId", "Start", "State" },
                values: new object[] { 1, 1, new DateTime(2021, 11, 20, 13, 45, 22, 193, DateTimeKind.Local).AddTicks(5098), null, 2, new DateTime(2021, 11, 15, 13, 45, 22, 193, DateTimeKind.Local).AddTicks(5128), 0 });

            migrationBuilder.InsertData(
                table: "RentingItems",
                columns: new[] { "ItemId", "RentingId", "Returned" },
                values: new object[] { 2, 1, false });
        }
    }
}
