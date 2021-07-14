using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class UpdateInternetSatisAddTarife : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Tarife",
                table: "H_InternetSatis",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "TarifeId",
                table: "H_InternetSatis",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "TarifeAktif",
                table: "H_Ayarlar",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tarife",
                table: "H_InternetSatis");

            migrationBuilder.DropColumn(
                name: "TarifeId",
                table: "H_InternetSatis");

            migrationBuilder.DropColumn(
                name: "TarifeAktif",
                table: "H_Ayarlar");
        }
    }
}
