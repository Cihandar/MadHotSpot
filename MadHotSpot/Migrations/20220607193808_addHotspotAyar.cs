using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class addHotspotAyar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "H_HotSpotAyar",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirmaId = table.Column<Guid>(nullable: false),
                    FirmaKodu = table.Column<int>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    UcretsizHotspot = table.Column<bool>(nullable: false),
                    MisafirEmail = table.Column<bool>(nullable: false),
                    MisafirEmailZorunlu = table.Column<bool>(nullable: false),
                    MisafirTelefon = table.Column<bool>(nullable: false),
                    MisafirTelefonZorunlu = table.Column<bool>(nullable: false),
                    ToplantiGirisi = table.Column<bool>(nullable: false),
                    ToplantiEmail = table.Column<bool>(nullable: false),
                    ToplantiEmailZorunlu = table.Column<bool>(nullable: false),
                    ToplantiTelefon = table.Column<bool>(nullable: false),
                    ToplantiTelefonZorunlu = table.Column<bool>(nullable: false),
                    PersonelGirisi = table.Column<bool>(nullable: false),
                    LogoUrl = table.Column<string>(nullable: true),
                    ArkaPlanUrl = table.Column<string>(nullable: true),
                    KvkkMetni = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_H_HotSpotAyar", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "H_HotSpotAyar");
        }
    }
}
