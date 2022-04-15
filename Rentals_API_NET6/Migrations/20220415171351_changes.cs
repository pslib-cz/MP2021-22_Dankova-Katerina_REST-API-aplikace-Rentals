using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentals_API_NET6.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OriginalName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OauthId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Trustfulness = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rentings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    ApproverId = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentings_Users_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rentings_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RentingHistoryLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentingId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ChangedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentingHistoryLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentingHistoryLogs_Rentings_RentingId",
                        column: x => x.RentingId,
                        principalTable: "Rentings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentingHistoryLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    RentingHistoryLogId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_Files_Img",
                        column: x => x.Img,
                        principalTable: "Files",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_RentingHistoryLogs_RentingHistoryLogId",
                        column: x => x.RentingHistoryLogId,
                        principalTable: "RentingHistoryLogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccessoryItems",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    AccessoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessoryItems", x => new { x.ItemId, x.AccessoryId });
                    table.ForeignKey(
                        name: "FK_AccessoryItems_Items_AccessoryId",
                        column: x => x.AccessoryId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessoryItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => new { x.ItemId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CartItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteItems",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteItems", x => new { x.ItemId, x.UserId });
                    table.ForeignKey(
                        name: "FK_FavouriteItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouriteItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "ItemHistoryLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChangedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false),
                    UserInventoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemHistoryLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemHistoryLogs_Items_UserInventoryId",
                        column: x => x.UserInventoryId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemHistoryLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemHistoryLogs_Users_UserInventoryId",
                        column: x => x.UserInventoryId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RentingItems",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    RentingId = table.Column<int>(type: "int", nullable: false),
                    Returned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentingItems", x => new { x.ItemId, x.RentingId });
                    table.ForeignKey(
                        name: "FK_RentingItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentingItems_Rentings_RentingId",
                        column: x => x.RentingId,
                        principalTable: "Rentings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemHistoryLogId = table.Column<int>(type: "int", nullable: false),
                    ChangedProperty = table.Column<int>(type: "int", nullable: false),
                    PreviousValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangedValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemChanges_ItemHistoryLogs_ItemHistoryLogId",
                        column: x => x.ItemHistoryLogId,
                        principalTable: "ItemHistoryLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemChangeConnections",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ItemChangeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemChangeConnections", x => new { x.ItemId, x.ItemChangeId });
                    table.ForeignKey(
                        name: "FK_ItemChangeConnections_ItemChanges_ItemChangeId",
                        column: x => x.ItemChangeId,
                        principalTable: "ItemChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemChangeConnections_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemPreChangeConnections",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ItemChangeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPreChangeConnections", x => new { x.ItemId, x.ItemChangeId });
                    table.ForeignKey(
                        name: "FK_ItemPreChangeConnections_ItemChanges_ItemChangeId",
                        column: x => x.ItemChangeId,
                        principalTable: "ItemChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemPreChangeConnections_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Přístroje" },
                    { 2, "Objektivy" },
                    { 3, "Stativy" },
                    { 4, "Příslušenství" },
                    { 5, "Audiotechnika" },
                    { 6, "Ostatní" }
                });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "OriginalName" },
                values: new object[,]
                {
                    { "AsteroidSprite_ddff.png", "AsteroidSprite_ddff" },
                    { "atelier_a27b.jpg", "atelier_a27b" },
                    { "BattChgLC-E6E_27dd.jpg", "BattChgLC-E6E_27dd" },
                    { "BattChgLC-E8E_02bb.jpg", "BattChgLC-E8E_02bb" },
                    { "BattLPE6_03b5.jpg", "BattLPE6_03b5" },
                    { "BattLPE8_fd96.jpg", "BattLPE8_fd96" },
                    { "CanonEF50_0055.jpg", "CanonEF50_0055" },
                    { "CanonEF70-200_b2f2.jpg", "CanonEF70-200_b2f2" },
                    { "comica1_5b38.jpg", "comica1_5b38" },
                    { "comica2_ed64.jpg", "comica2_ed64" },
                    { "crane3Lab_1685.jpg", "crane3Lab_1685" },
                    { "Cullmann3400_cd56.jpg", "Cullmann3400_cd56" },
                    { "cvm-d02_8d19.jpg", "cvm-d02_8d19" },
                    { "deity_v-mic-d3_e4bb.jpg", "deity_v-mic-d3_e4bb" },
                    { "e890d2d1-ce7d-4b33-98ff-851e85ac4fd2_940c.jpg", "e890d2d1-ce7d-4b33-98ff-851e85ac4fd2_940c" },
                    { "e890d2d1-ce7d-4b33-98ff-851e85ac4fd2_b249.jpg", "e890d2d1-ce7d-4b33-98ff-851e85ac4fd2_b249" },
                    { "EOS_RP_e3fe.jpg", "EOS_RP_e3fe" },
                    { "EOS650D_5a40.jpg", "EOS650D_5a40" },
                    { "EOS70D_b624.jpg", "EOS70D_b624" },
                    { "EOS90D_7dd9.jpg", "EOS90D_7dd9" },
                    { "flycam_fd0f.jpg", "flycam_fd0f" },
                    { "gripC70D_2982.jpg", "gripC70D_2982" },
                    { "Group 1_0aad.jpg", "Group 1_0aad" },
                    { "Group 612_3ac5.png", "Group 612_3ac5" },
                    { "hamaStar62_914d.jpg", "hamaStar62_914d" },
                    { "img_2_46b9.jpg", "img_2_46b9" },
                    { "img_2_de6d.jpg", "img_2_de6d" },
                    { "kitt-design-illustration_73ad.png", "kitt-design-illustration_73ad" },
                    { "MikrofonKlopový_97b3.jpg", "MikrofonKlopový_97b3" },
                    { "MiniTripod_92cc.jpg", "MiniTripod_92cc" },
                    { "moza_912f.jpg", "moza_912f" },
                    { "Panasonic_HC-X920_7ce7.jpg", "Panasonic_HC-X920_7ce7" },
                    { "PanasonicVW-CT45E_cd22.jpg", "PanasonicVW-CT45E_cd22" },
                    { "Placeholder.jpg", "Placeholder" },
                    { "rigHawk_8fd1.jpg", "rigHawk_8fd1" },
                    { "RigSpider_ea93.jpg", "RigSpider_ea93" }
                });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "OriginalName" },
                values: new object[,]
                {
                    { "SDLexar64GB_e4c2.jpg", "SDLexar64GB_e4c2" },
                    { "SDSanDisk64GB_2b06.jpg", "SDSanDisk64GB_2b06" },
                    { "SDSanDisk64GB_715b.jpg", "SDSanDisk64GB_715b" },
                    { "Sigma18-200_cd74.jpg", "Sigma18-200_cd74" },
                    { "Sigma18-50_f918.jpg", "Sigma18-50_f918" },
                    { "Sigma24-70_7bde.jpg", "Sigma24-70_7bde" },
                    { "Sigma30_0caf.jpg", "Sigma30_0caf" },
                    { "snoppa_4626.jpg", "snoppa_4626" },
                    { "SonyHDR-CX320_6824.jpg", "SonyHDR-CX320_6824" },
                    { "SonyVct-d680rm_9d51.jpg", "SonyVct-d680rm_9d51" },
                    { "stativ-svetlo_98ea.jpg", "stativ-svetlo_98ea" },
                    { "StativMS-007H_3d14_d8f7.png", "StativMS-007H_3d14_d8f7" },
                    { "StativMS-007H_3d14.jpg", "StativMS-007H_3d14" },
                    { "StativVelbonC-600_b14c.jpg", "StativVelbonC-600_b14c" },
                    { "tn-techtips_f9dd.jpg", "tn-techtips_f9dd" },
                    { "Viltrox_f0a5.jpg", "Viltrox_f0a5" },
                    { "ZoomH1_2ffd.jpg", "ZoomH1_2ffd" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CategoryId", "Description", "Img", "IsDeleted", "Name", "Note", "RentingHistoryLogId", "State" },
                values: new object[,]
                {
                    { 1, 1, "Kamera", null, true, "Kamera", null, null, 0 },
                    { 2, 1, "Kamera", null, true, "Kamera", null, null, 0 },
                    { 3, 5, "Dobrý zvuk", null, true, "Mikrofon", null, null, 0 },
                    { 4, 5, "Dobrý zvuk", null, true, "Mikrofon", null, null, 0 },
                    { 5, 1, "24,1 Mpx", null, true, "Kamera Sony", null, null, 0 },
                    { 6, 1, "24,1 Mpx", null, true, "Kamera Sony", null, null, 0 },
                    { 7, 1, "24,1 Mpx", null, true, "Kamera Sony", null, null, 0 },
                    { 8, 1, "Lehké tělo, kompaktní, s možností až 4K videa", null, true, "Zrcadlovka SONY Alpha A6300", null, null, 0 },
                    { 9, 1, "Lehké tělo, kompaktní, s možností až 4K videa", null, true, "Zrcadlovka SONY Alpha A6300", null, null, 0 },
                    { 10, 1, "Lehké tělo, kompaktní, s možností až 4K videa", null, true, "Zrcadlovka SONY Alpha A6300", null, null, 0 },
                    { 11, 1, null, "EOS650D_5a40.jpg", false, "Fotoaparát Canon EOS 650D", "Bez očnice", null, 0 },
                    { 12, 1, null, "EOS650D_5a40.jpg", false, "Fotoaparát Canon EOS 650D", "Bez očnice", null, 0 },
                    { 13, 2, null, "Sigma18-50_f918.jpg", false, "Objektiv SIGMA 17-50 mm 1:2.8", "Prstenec transfokátoru má vůli", null, 0 },
                    { 14, 2, null, "Sigma18-50_f918.jpg", false, "Objektiv SIGMA 17-50 mm 1:2.8", "Určen primárně do ateliéru!", null, 0 },
                    { 15, 1, null, "EOS70D_b624.jpg", false, "Fotoaparát Canon EOS 70D", null, null, 0 },
                    { 16, 1, null, "EOS70D_b624.jpg", false, "Fotoaparát Canon EOS 70D", "Určen primárně do ateliéru!", null, 0 },
                    { 17, 2, "FullFrame objektiv", "Sigma24-70_7bde.jpg", false, "Objektiv SIGMA 24-70 mm 1:2.8", null, null, 0 },
                    { 18, 1, "Stříbrná", null, false, "Videokamera Sony 1.9/2.1-57", null, null, 0 },
                    { 19, 1, "Stříbrná", null, false, "Videokamera Sony 1.9/2.1-57", null, null, 0 },
                    { 20, 1, "Stříbrná", null, false, "Videokamera Sony 1.9/2.1-57", null, null, 0 },
                    { 21, 1, "F1.8/f1.9-57 (černá)", "SonyHDR-CX320_6824.jpg", false, "Videokamera Sony HDR-CX320", null, null, 0 },
                    { 22, 4, null, "gripC70D_2982.jpg", false, "Bateriový grip Phottix BG 70D", null, null, 0 },
                    { 23, 4, null, null, true, "Knoflíková baterie GP Alkaline A76 LR44 V13GA 1.5 V", null, null, 0 },
                    { 24, 4, "90 MB/s", "SDSanDisk64GB_715b.jpg", false, "SD karta SanDisk 64 GB", null, null, 0 },
                    { 25, 4, "90 MB/s", "SDSanDisk64GB_715b.jpg", false, "SD karta SanDisk 64 GB", null, null, 0 },
                    { 26, 4, null, null, true, "Alkalická baterie Alkalisk 1.5 V LR03 AAA Ikea", null, null, 0 },
                    { 27, 4, null, null, true, "Alkalická baterie Alkalisk 1.5 V LR03 AAA Ikea", null, null, 0 },
                    { 28, 4, null, null, true, "Alkalická baterie Eneloop 1.2 V HR 3UTG AA", null, null, 0 },
                    { 29, 4, null, null, true, "Alkalická baterie Eneloop 1.2 V HR 4UTG AAA", null, null, 0 },
                    { 30, 4, null, null, true, "Alkalická baterie LSD 1.2 V AAA", null, null, 0 },
                    { 31, 4, null, null, true, "Alkalická baterie LSD 1.2 V AAA", null, null, 0 },
                    { 32, 4, null, null, true, "Alkalická baterie LSD 1.2 V AAA", null, null, 0 },
                    { 33, 4, null, null, true, "Alkalická baterie LSD 1.2 V AA", null, null, 0 },
                    { 34, 4, null, null, true, "Alkalická baterie LSD 1.2 V AA", null, null, 0 },
                    { 35, 4, null, null, true, "Alkalická baterie LSD 1.2 V AA", null, null, 0 },
                    { 36, 4, null, null, true, "Alkalická baterie LSD 1.2 V AA", null, null, 0 },
                    { 37, 4, null, null, true, "Alkalická baterie LSD 1.2 V AA", null, null, 0 },
                    { 38, 4, "Černý", null, true, "MicroSD karta adapter SAMSUNG", null, null, 0 },
                    { 39, 4, "Černý", null, false, "SD karta Kingston SD10G3 32 GB", null, null, 0 },
                    { 40, 4, "Průhledné", null, true, "Pouzdro na SD kartu", null, null, 0 },
                    { 41, 4, "Průhledné", null, true, "Pouzdro na SD kartu", null, null, 0 },
                    { 42, 4, "Průhledné", null, true, "Pouzdro na SD kartu", null, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CategoryId", "Description", "Img", "IsDeleted", "Name", "Note", "RentingHistoryLogId", "State" },
                values: new object[,]
                {
                    { 43, 4, "Průhledné", null, true, "Pouzdro na SD kartu", null, null, 0 },
                    { 44, 2, "FullFrame", "CanonEF70-200_b2f2.jpg", false, "Objektiv Canon ULTRASONIC 70-200 mm F4", null, null, 0 },
                    { 45, 1, "Černý", null, false, "Fotoaparát Canon EOS 350D", null, null, 0 },
                    { 46, 2, "FullFrame", "CanonEF50_0055.jpg", false, "Objektiv Canon 50 mm F1.8", null, null, 0 },
                    { 47, 4, "Černá", null, false, "Baterie SONY NP FV70A 1900mAh", null, null, 0 },
                    { 48, 4, "Černá", null, true, "Baterie Canon LP E6N 1865mAh", null, null, 0 },
                    { 49, 4, "Černá", null, true, "Baterie Canon LP E6 1800mAh", null, null, 0 },
                    { 50, 4, "k 650D (šedá)", "BattLPE8_fd96.jpg", false, "Baterie Canon LP E8 1120mAh", null, null, 0 },
                    { 51, 4, "k 650D (šedá)", "BattLPE8_fd96.jpg", false, "Baterie Canon LP E8 1120mAh", null, null, 0 },
                    { 52, 4, "k 650D (šedá)", "BattLPE8_fd96.jpg", false, "Baterie Canon LP E8 1120mAh", null, null, 0 },
                    { 53, 4, "k C70D, C5D", "BattLPE6_03b5.jpg", false, "Baterie Canon LP E6 1800mAh", null, null, 0 },
                    { 54, 4, "k C70D, C5D", "BattLPE6_03b5.jpg", false, "Baterie Canon LP E6 1800mAh", null, null, 0 },
                    { 55, 4, "k C70D, C5D", "BattLPE6_03b5.jpg", false, "Baterie Canon LP E6 1800mAh", null, null, 0 },
                    { 56, 4, "Černá", null, true, "Baterie Canon LP E6 1865mAh", null, null, 0 },
                    { 57, 4, "Černá", null, false, "Baterie SONY NP FV30 500mAh", null, null, 0 },
                    { 58, 4, "Černá", null, false, "Baterie SONY NP FV30 500mAh", null, null, 0 },
                    { 59, 4, "Černá", null, false, "Baterie SONY NP FV30 500mAh", null, null, 0 },
                    { 60, 4, "náhradní akumulátor", null, false, "Baterie Panasonic VW VBN130 1250mAh", null, null, 0 },
                    { 61, 4, "k C70D, C5D", "BattChgLC-E6E_27dd.jpg", false, "Nabíječka baterií Canon LC E6E", null, null, 0 },
                    { 62, 4, "k C70D, C5D", "BattChgLC-E6E_27dd.jpg", false, "Nabíječka baterií Canon LC E6E", null, null, 0 },
                    { 63, 4, "k C650D", "BattChgLC-E8E_02bb.jpg", false, "Nabíječka baterií Canon LC E8E", null, null, 0 },
                    { 64, 4, "k C650D", "BattChgLC-E8E_02bb.jpg", false, "Nabíječka baterií Canon LC E8E", null, null, 0 },
                    { 65, 4, "Černá", null, false, "Nabíječka baterií FK technics BC 450", null, null, 0 },
                    { 66, 4, "Černý", null, true, "Napájecí kabel I SHENG 033", null, null, 0 },
                    { 67, 4, "Černý", null, true, "Napájecí kabel I SHENG 033", null, null, 0 },
                    { 68, 6, null, null, true, "Testujeme s.r.o.", null, null, 0 },
                    { 69, 2, "(Kz)", "Sigma18-200_cd74.jpg", false, "Objektiv SIGMA 18-200 mm 1:3.5-6.3", null, null, 0 },
                    { 70, 3, null, "StativVelbonC-600_b14c.jpg", false, "Stativ Velbon C-600", "xxx 2308", null, 0 },
                    { 71, 3, null, "StativVelbonC-600_b14c.jpg", false, "Stativ Velbon C-600", null, null, 0 },
                    { 72, 4, "95 MB/s", "SDLexar64GB_e4c2.jpg", false, "SD karta Lexar 64GB", null, null, 0 },
                    { 73, 6, "Rezervace místnosti se studiovými světly a odpalovačem.", "atelier_a27b.jpg", false, "Ateliér B210", null, null, 0 },
                    { 74, 3, "Hlava má vůli; nevhodný na video", "hamaStar62_914d.jpg", false, "Stativ Hama Star 62", null, null, 0 },
                    { 75, 3, "Steadicam Camtree Wonder-3", "flycam_fd0f.jpg", false, "Flycam HD-3000", null, null, 0 },
                    { 76, 5, null, "ZoomH1_2ffd.jpg", false, "Rekordér Zoom H1", null, null, 0 },
                    { 77, 5, null, "ZoomH1_2ffd.jpg", false, "Rekordér Zoom H1", null, null, 0 },
                    { 78, 3, null, "MiniTripod_92cc.jpg", false, "stativ MiniTripod plochý", null, null, 0 },
                    { 79, 2, null, "Sigma30_0caf.jpg", false, "Objektiv Sigma 30mm/F1.4", null, null, 0 },
                    { 80, 1, "F1,5 f2,84-34,1mm (včetně akumulátoru)", "Panasonic_HC-X920_7ce7.jpg", false, "Videokamera Panasonic HC-X920", null, null, 0 },
                    { 81, 3, null, "RigSpider_ea93.jpg", false, "Stativ Rig Spider FT-10", null, null, 0 },
                    { 82, 3, null, "RigSpider_ea93.jpg", false, "Stativ Rig Spider FT-10", null, null, 0 },
                    { 83, 3, null, "RigSpider_ea93.jpg", false, "Stativ Rig Spider FT-10", null, null, 0 },
                    { 84, 3, "(kulová hlava)", "StativMS-007H_3d14.jpg", false, "Stativ video MS-007H", "poškozen zip", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CategoryId", "Description", "Img", "IsDeleted", "Name", "Note", "RentingHistoryLogId", "State" },
                values: new object[,]
                {
                    { 85, 5, "bez předzesilovače, induktivní", "MikrofonKlopový_97b3.jpg", false, "Mikrofon klopový", null, null, 0 },
                    { 86, 5, "bez předzesilovače, induktivní", "MikrofonKlopový_97b3.jpg", false, "Mikrofon klopový", null, null, 0 },
                    { 87, 3, null, "SonyVct-d680rm_9d51.jpg", false, "Stativ Sony VCT-D680RM", null, null, 0 },
                    { 88, 6, "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché až primitivní konstrukci nabývá minimalistického a elegantního vzhledu. ", "tn-techtips_f9dd.jpg", true, "zkušební předmět", "první předmět", null, 0 },
                    { 89, 6, "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché až primitivní konstrukci nabývá minimalistického a elegantního vzhledu. ", "kitt-design-illustration_73ad.png", true, "zkušební předmět", "druhý předmět", null, 0 },
                    { 90, 6, "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché až primitivní konstrukci nabývá minimalistického a elegantního vzhledu. ", "tn-techtips_f9dd.jpg", true, "zkušební předmět", null, null, 0 },
                    { 91, 6, "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché konstrukci nabývá minimalistického a elegantního vzhledu. ", null, true, "Fiktivní předmět", null, null, 0 },
                    { 92, 6, "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché konstrukci nabývá minimalistického a elegantního vzhledu. ", null, true, "Fiktivní předmět", null, null, 0 },
                    { 93, 4, null, null, true, "Alkalická baterie GP 2700 1.2 V AA", null, null, 0 },
                    { 94, 4, null, null, true, "Alkalická baterie GP 2700 1.2 V AA", null, null, 0 },
                    { 95, 4, null, null, true, "Alkalická baterie GP 2700 1.2 V AA", null, null, 0 }
                });

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
                    { 93, 65 },
                    { 94, 65 },
                    { 95, 65 },
                    { 78, 76 },
                    { 78, 77 },
                    { 60, 80 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessoryItems_AccessoryId",
                table: "AccessoryItems",
                column: "AccessoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserId",
                table: "CartItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteItems_UserId",
                table: "FavouriteItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_UserId",
                table: "InventoryItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemHistoryLogs_UserId",
                table: "ItemHistoryLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemHistoryLogs_UserInventoryId",
                table: "ItemHistoryLogs",
                column: "UserInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemChangeConnections_ItemChangeId",
                table: "ItemChangeConnections",
                column: "ItemChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemChanges_ItemHistoryLogId",
                table: "ItemChanges",
                column: "ItemHistoryLogId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPreChangeConnections_ItemChangeId",
                table: "ItemPreChangeConnections",
                column: "ItemChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Img",
                table: "Items",
                column: "Img");

            migrationBuilder.CreateIndex(
                name: "IX_Items_RentingHistoryLogId",
                table: "Items",
                column: "RentingHistoryLogId");

            migrationBuilder.CreateIndex(
                name: "IX_RentingHistoryLogs_RentingId",
                table: "RentingHistoryLogs",
                column: "RentingId");

            migrationBuilder.CreateIndex(
                name: "IX_RentingHistoryLogs_UserId",
                table: "RentingHistoryLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RentingItems_RentingId",
                table: "RentingItems",
                column: "RentingId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentings_ApproverId",
                table: "Rentings",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentings_OwnerId",
                table: "Rentings",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OauthId",
                table: "Users",
                column: "OauthId",
                unique: true,
                filter: "[OauthId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessoryItems");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "FavouriteItems");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropTable(
                name: "ItemChangeConnections");

            migrationBuilder.DropTable(
                name: "ItemPreChangeConnections");

            migrationBuilder.DropTable(
                name: "RentingItems");

            migrationBuilder.DropTable(
                name: "ItemChanges");

            migrationBuilder.DropTable(
                name: "ItemHistoryLogs");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "RentingHistoryLogs");

            migrationBuilder.DropTable(
                name: "Rentings");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
