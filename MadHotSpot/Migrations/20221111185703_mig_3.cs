using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class mig_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GunuBirlikZorunlu",
                table: "H_HotSpotAyar",
                newName: "GunuBirlikEmailZorunlu");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "H_CustomerInfo",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "H_Logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirmaId = table.Column<Guid>(nullable: false),
                    FirmaKodu = table.Column<int>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Module = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Error = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_H_Logs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "H_Logs");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "H_CustomerInfo");

            migrationBuilder.RenameColumn(
                name: "GunuBirlikEmailZorunlu",
                table: "H_HotSpotAyar",
                newName: "GunuBirlikZorunlu");
        }
    }
}
