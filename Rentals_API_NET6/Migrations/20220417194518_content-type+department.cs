using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentals_API_NET6.Migrations
{
    public partial class contenttypedepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearOfEntry",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "AsteroidSprite_ddff.png",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/png", "AsteroidSprite_ddff.png" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "atelier_a27b.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "atelier_a27b.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "BattChgLC-E6E_27dd.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "BattChgLC-E6E_27dd.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "BattChgLC-E8E_02bb.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "BattChgLC-E8E_02bb.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "BattLPE6_03b5.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "BattLPE6_03b5.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "BattLPE8_fd96.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "BattLPE8_fd96.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "CanonEF50_0055.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "CanonEF50_0055.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "CanonEF70-200_b2f2.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "CanonEF70-200_b2f2.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "comica1_5b38.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "comica1_5b38.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "comica2_ed64.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "comica2_ed64.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "crane3Lab_1685.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "crane3Lab_1685.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Cullmann3400_cd56.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "Cullmann3400_cd56.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "cvm-d02_8d19.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "cvm-d02_8d19.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "deity_v-mic-d3_e4bb.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "deity_v-mic-d3_e4bb.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "e890d2d1-ce7d-4b33-98ff-851e85ac4fd2_940c.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "e890d2d1-ce7d-4b33-98ff-851e85ac4fd2_940c.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "e890d2d1-ce7d-4b33-98ff-851e85ac4fd2_b249.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "e890d2d1-ce7d-4b33-98ff-851e85ac4fd2_b249.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "EOS_RP_e3fe.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "EOS_RP_e3fe.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "EOS650D_5a40.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "EOS650D_5a40.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "EOS70D_b624.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "EOS70D_b624.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "EOS90D_7dd9.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "EOS90D_7dd9.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "flycam_fd0f.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "flycam_fd0f.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "gripC70D_2982.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "gripC70D_2982.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Group 1_0aad.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "Group 1_0aad.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Group 612_3ac5.png",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/png", "Group 612_3ac5.png" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "hamaStar62_914d.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "hamaStar62_914d.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "img_2_46b9.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "img_2_46b9.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "img_2_de6d.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "img_2_de6d.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "kitt-design-illustration_73ad.png",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/png", "kitt-design-illustration_73ad.png" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "MikrofonKlopový_97b3.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "MikrofonKlopový_97b3.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "MiniTripod_92cc.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "MiniTripod_92cc.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "moza_912f.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "moza_912f.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Panasonic_HC-X920_7ce7.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "Panasonic_HC-X920_7ce7.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "PanasonicVW-CT45E_cd22.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "PanasonicVW-CT45E_cd22.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Placeholder.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "Placeholder.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "rigHawk_8fd1.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "rigHawk_8fd1.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "RigSpider_ea93.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "RigSpider_ea93.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "SDLexar64GB_e4c2.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "SDLexar64GB_e4c2.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "SDSanDisk64GB_2b06.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "SDSanDisk64GB_2b06.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "SDSanDisk64GB_715b.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "SDSanDisk64GB_715b.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Sigma18-200_cd74.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "Sigma18-200_cd74.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Sigma18-50_f918.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "Sigma18-50_f918.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Sigma24-70_7bde.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "Sigma24-70_7bde.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Sigma30_0caf.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "Sigma30_0caf.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "snoppa_4626.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "snoppa_4626.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "SonyHDR-CX320_6824.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "SonyHDR-CX320_6824.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "SonyVct-d680rm_9d51.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "SonyVct-d680rm_9d51.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "stativ-svetlo_98ea.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "stativ-svetlo_98ea.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "StativMS-007H_3d14_d8f7.png",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/png", "StativMS-007H_3d14_d8f7.png" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "StativMS-007H_3d14.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "StativMS-007H_3d14.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "StativVelbonC-600_b14c.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "StativVelbonC-600_b14c.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "tn-techtips_f9dd.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "tn-techtips_f9dd.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Viltrox_f0a5.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "Viltrox_f0a5.jpg" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "ZoomH1_2ffd.jpg",
                columns: new[] { "ContentType", "OriginalName" },
                values: new object[] { "image/jpeg", "ZoomH1_2ffd.jpg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "YearOfEntry",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Files");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "AsteroidSprite_ddff.png",
                column: "OriginalName",
                value: "AsteroidSprite_ddff");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "atelier_a27b.jpg",
                column: "OriginalName",
                value: "atelier_a27b");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "BattChgLC-E6E_27dd.jpg",
                column: "OriginalName",
                value: "BattChgLC-E6E_27dd");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "BattChgLC-E8E_02bb.jpg",
                column: "OriginalName",
                value: "BattChgLC-E8E_02bb");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "BattLPE6_03b5.jpg",
                column: "OriginalName",
                value: "BattLPE6_03b5");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "BattLPE8_fd96.jpg",
                column: "OriginalName",
                value: "BattLPE8_fd96");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "CanonEF50_0055.jpg",
                column: "OriginalName",
                value: "CanonEF50_0055");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "CanonEF70-200_b2f2.jpg",
                column: "OriginalName",
                value: "CanonEF70-200_b2f2");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "comica1_5b38.jpg",
                column: "OriginalName",
                value: "comica1_5b38");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "comica2_ed64.jpg",
                column: "OriginalName",
                value: "comica2_ed64");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "crane3Lab_1685.jpg",
                column: "OriginalName",
                value: "crane3Lab_1685");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Cullmann3400_cd56.jpg",
                column: "OriginalName",
                value: "Cullmann3400_cd56");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "cvm-d02_8d19.jpg",
                column: "OriginalName",
                value: "cvm-d02_8d19");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "deity_v-mic-d3_e4bb.jpg",
                column: "OriginalName",
                value: "deity_v-mic-d3_e4bb");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "e890d2d1-ce7d-4b33-98ff-851e85ac4fd2_940c.jpg",
                column: "OriginalName",
                value: "e890d2d1-ce7d-4b33-98ff-851e85ac4fd2_940c");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "e890d2d1-ce7d-4b33-98ff-851e85ac4fd2_b249.jpg",
                column: "OriginalName",
                value: "e890d2d1-ce7d-4b33-98ff-851e85ac4fd2_b249");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "EOS_RP_e3fe.jpg",
                column: "OriginalName",
                value: "EOS_RP_e3fe");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "EOS650D_5a40.jpg",
                column: "OriginalName",
                value: "EOS650D_5a40");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "EOS70D_b624.jpg",
                column: "OriginalName",
                value: "EOS70D_b624");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "EOS90D_7dd9.jpg",
                column: "OriginalName",
                value: "EOS90D_7dd9");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "flycam_fd0f.jpg",
                column: "OriginalName",
                value: "flycam_fd0f");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "gripC70D_2982.jpg",
                column: "OriginalName",
                value: "gripC70D_2982");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Group 1_0aad.jpg",
                column: "OriginalName",
                value: "Group 1_0aad");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Group 612_3ac5.png",
                column: "OriginalName",
                value: "Group 612_3ac5");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "hamaStar62_914d.jpg",
                column: "OriginalName",
                value: "hamaStar62_914d");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "img_2_46b9.jpg",
                column: "OriginalName",
                value: "img_2_46b9");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "img_2_de6d.jpg",
                column: "OriginalName",
                value: "img_2_de6d");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "kitt-design-illustration_73ad.png",
                column: "OriginalName",
                value: "kitt-design-illustration_73ad");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "MikrofonKlopový_97b3.jpg",
                column: "OriginalName",
                value: "MikrofonKlopový_97b3");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "MiniTripod_92cc.jpg",
                column: "OriginalName",
                value: "MiniTripod_92cc");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "moza_912f.jpg",
                column: "OriginalName",
                value: "moza_912f");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Panasonic_HC-X920_7ce7.jpg",
                column: "OriginalName",
                value: "Panasonic_HC-X920_7ce7");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "PanasonicVW-CT45E_cd22.jpg",
                column: "OriginalName",
                value: "PanasonicVW-CT45E_cd22");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Placeholder.jpg",
                column: "OriginalName",
                value: "Placeholder");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "rigHawk_8fd1.jpg",
                column: "OriginalName",
                value: "rigHawk_8fd1");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "RigSpider_ea93.jpg",
                column: "OriginalName",
                value: "RigSpider_ea93");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "SDLexar64GB_e4c2.jpg",
                column: "OriginalName",
                value: "SDLexar64GB_e4c2");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "SDSanDisk64GB_2b06.jpg",
                column: "OriginalName",
                value: "SDSanDisk64GB_2b06");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "SDSanDisk64GB_715b.jpg",
                column: "OriginalName",
                value: "SDSanDisk64GB_715b");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Sigma18-200_cd74.jpg",
                column: "OriginalName",
                value: "Sigma18-200_cd74");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Sigma18-50_f918.jpg",
                column: "OriginalName",
                value: "Sigma18-50_f918");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Sigma24-70_7bde.jpg",
                column: "OriginalName",
                value: "Sigma24-70_7bde");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Sigma30_0caf.jpg",
                column: "OriginalName",
                value: "Sigma30_0caf");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "snoppa_4626.jpg",
                column: "OriginalName",
                value: "snoppa_4626");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "SonyHDR-CX320_6824.jpg",
                column: "OriginalName",
                value: "SonyHDR-CX320_6824");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "SonyVct-d680rm_9d51.jpg",
                column: "OriginalName",
                value: "SonyVct-d680rm_9d51");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "stativ-svetlo_98ea.jpg",
                column: "OriginalName",
                value: "stativ-svetlo_98ea");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "StativMS-007H_3d14_d8f7.png",
                column: "OriginalName",
                value: "StativMS-007H_3d14_d8f7");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "StativMS-007H_3d14.jpg",
                column: "OriginalName",
                value: "StativMS-007H_3d14");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "StativVelbonC-600_b14c.jpg",
                column: "OriginalName",
                value: "StativVelbonC-600_b14c");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "tn-techtips_f9dd.jpg",
                column: "OriginalName",
                value: "tn-techtips_f9dd");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "Viltrox_f0a5.jpg",
                column: "OriginalName",
                value: "Viltrox_f0a5");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "ZoomH1_2ffd.jpg",
                column: "OriginalName",
                value: "ZoomH1_2ffd");
        }
    }
}
