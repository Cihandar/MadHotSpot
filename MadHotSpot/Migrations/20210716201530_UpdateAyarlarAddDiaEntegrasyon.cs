using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class UpdateAyarlarAddDiaEntegrasyon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DiaEntegrasyonAktif",
                table: "H_Ayarlar",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DiaUrl",
                table: "H_Ayarlar",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaEntegrasyonAktif",
                table: "H_Ayarlar");

            migrationBuilder.DropColumn(
                name: "DiaUrl",
                table: "H_Ayarlar");
        }
    }
}
