using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class UpdateAyarlarAddIadeAktif : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IadeAktif",
                table: "H_Ayarlar",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IadeAktif",
                table: "H_Ayarlar");
        }
    }
}
