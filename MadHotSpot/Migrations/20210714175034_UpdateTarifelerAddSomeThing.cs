using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class UpdateTarifelerAddSomeThing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Doviz",
                table: "H_Tarifeler");

            migrationBuilder.RenameColumn(
                name: "Fiyat",
                table: "H_Tarifeler",
                newName: "USDFiyat");

            migrationBuilder.AddColumn<bool>(
                name: "Aktif",
                table: "H_Tarifeler",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "EUROFiyat",
                table: "H_Tarifeler",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TLFiyat",
                table: "H_Tarifeler",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aktif",
                table: "H_Tarifeler");

            migrationBuilder.DropColumn(
                name: "EUROFiyat",
                table: "H_Tarifeler");

            migrationBuilder.DropColumn(
                name: "TLFiyat",
                table: "H_Tarifeler");

            migrationBuilder.RenameColumn(
                name: "USDFiyat",
                table: "H_Tarifeler",
                newName: "Fiyat");

            migrationBuilder.AddColumn<string>(
                name: "Doviz",
                table: "H_Tarifeler",
                nullable: true);
        }
    }
}
