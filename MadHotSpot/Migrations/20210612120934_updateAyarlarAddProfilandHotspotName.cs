using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class updateAyarlarAddProfilandHotspotName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MikrotikHotspotAdi",
                table: "H_Ayarlar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MikrotikProfilAdi",
                table: "H_Ayarlar",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MikrotikHotspotAdi",
                table: "H_Ayarlar");

            migrationBuilder.DropColumn(
                name: "MikrotikProfilAdi",
                table: "H_Ayarlar");
        }
    }
}
