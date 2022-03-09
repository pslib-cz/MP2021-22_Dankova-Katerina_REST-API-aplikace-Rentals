using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentals_API_NET6.Migrations
{
    public partial class files : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Uploaded",
                table: "Files");

            migrationBuilder.AlterColumn<string>(
                name: "Img",
                table: "Items",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
                    { "RigSpider_ea93.jpg", "RigSpider_ea93" },
                    { "SDLexar64GB_e4c2.jpg", "SDLexar64GB_e4c2" },
                    { "SDSanDisk64GB_2b06.jpg", "SDSanDisk64GB_2b06" },
                    { "SDSanDisk64GB_715b.jpg", "SDSanDisk64GB_715b" },
                    { "Sigma18-200_cd74.jpg", "Sigma18-200_cd74" },
                    { "Sigma18-50_f918.jpg", "Sigma18-50_f918" },
                    { "Sigma24-70_7bde.jpg", "Sigma24-70_7bde" }
                });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "OriginalName" },
                values: new object[,]
                {
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

            migrationBuilder.CreateIndex(
                name: "IX_Items_Img",
                table: "Items",
                column: "Img");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Files_Img",
                table: "Items",
                column: "Img",
                principalTable: "Files",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Files_Img",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Img",
                table: "Items");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "AsteroidSprite_ddff.png");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "atelier_a27b.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "BattChgLC-E6E_27dd.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "BattChgLC-E8E_02bb.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "BattLPE6_03b5.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "BattLPE8_fd96.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "CanonEF50_0055.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "CanonEF70-200_b2f2.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "comica1_5b38.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "comica2_ed64.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "crane3Lab_1685.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Cullmann3400_cd56.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "cvm-d02_8d19.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "deity_v-mic-d3_e4bb.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "e890d2d1-ce7d-4b33-98ff-851e85ac4fd2_940c.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "e890d2d1-ce7d-4b33-98ff-851e85ac4fd2_b249.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "EOS_RP_e3fe.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "EOS650D_5a40.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "EOS70D_b624.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "EOS90D_7dd9.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "flycam_fd0f.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "gripC70D_2982.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Group 1_0aad.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Group 612_3ac5.png");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "hamaStar62_914d.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "img_2_46b9.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "img_2_de6d.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "kitt-design-illustration_73ad.png");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "MikrofonKlopový_97b3.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "MiniTripod_92cc.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "moza_912f.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Panasonic_HC-X920_7ce7.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "PanasonicVW-CT45E_cd22.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Placeholder.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "rigHawk_8fd1.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "RigSpider_ea93.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "SDLexar64GB_e4c2.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "SDSanDisk64GB_2b06.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "SDSanDisk64GB_715b.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Sigma18-200_cd74.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Sigma18-50_f918.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Sigma24-70_7bde.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Sigma30_0caf.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "snoppa_4626.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "SonyHDR-CX320_6824.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "SonyVct-d680rm_9d51.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "stativ-svetlo_98ea.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "StativMS-007H_3d14_d8f7.png");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "StativMS-007H_3d14.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "StativVelbonC-600_b14c.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "tn-techtips_f9dd.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Viltrox_f0a5.jpg");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "ZoomH1_2ffd.jpg");

            migrationBuilder.AlterColumn<string>(
                name: "Img",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "Files",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "Uploaded",
                table: "Files",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
