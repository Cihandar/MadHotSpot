using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class mig_Elektra_HotsPotMode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ElektraFreeProfile",
                table: "H_Ayarlar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ElektraHighSpeedProfile",
                table: "H_Ayarlar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ElektraPaidProfile",
                table: "H_Ayarlar",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElektraFreeProfile",
                table: "H_Ayarlar");

            migrationBuilder.DropColumn(
                name: "ElektraHighSpeedProfile",
                table: "H_Ayarlar");

            migrationBuilder.DropColumn(
                name: "ElektraPaidProfile",
                table: "H_Ayarlar");
        }
    }
}
