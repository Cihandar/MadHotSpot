using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class mig_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GunuBirlikEmail",
                table: "H_HotSpotAyar",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GunuBirlikGirisi",
                table: "H_HotSpotAyar",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GunuBirlikTelefon",
                table: "H_HotSpotAyar",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GunuBirlikTelefonZorunlu",
                table: "H_HotSpotAyar",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GunuBirlikZorunlu",
                table: "H_HotSpotAyar",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SpaEmail",
                table: "H_HotSpotAyar",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SpaEmailZorunlu",
                table: "H_HotSpotAyar",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SpaGirisi",
                table: "H_HotSpotAyar",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SpaTelefon",
                table: "H_HotSpotAyar",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SpaTelefonZorunlu",
                table: "H_HotSpotAyar",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GunuBirlikEmail",
                table: "H_HotSpotAyar");

            migrationBuilder.DropColumn(
                name: "GunuBirlikGirisi",
                table: "H_HotSpotAyar");

            migrationBuilder.DropColumn(
                name: "GunuBirlikTelefon",
                table: "H_HotSpotAyar");

            migrationBuilder.DropColumn(
                name: "GunuBirlikTelefonZorunlu",
                table: "H_HotSpotAyar");

            migrationBuilder.DropColumn(
                name: "GunuBirlikZorunlu",
                table: "H_HotSpotAyar");

            migrationBuilder.DropColumn(
                name: "SpaEmail",
                table: "H_HotSpotAyar");

            migrationBuilder.DropColumn(
                name: "SpaEmailZorunlu",
                table: "H_HotSpotAyar");

            migrationBuilder.DropColumn(
                name: "SpaGirisi",
                table: "H_HotSpotAyar");

            migrationBuilder.DropColumn(
                name: "SpaTelefon",
                table: "H_HotSpotAyar");

            migrationBuilder.DropColumn(
                name: "SpaTelefonZorunlu",
                table: "H_HotSpotAyar");
        }
    }
}
