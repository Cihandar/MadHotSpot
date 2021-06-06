using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class FirstDbSend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "H_Ayarlar",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirmaId = table.Column<Guid>(nullable: false),
                    FirmaKodu = table.Column<int>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    SinirsizAktif = table.Column<bool>(nullable: false),
                    GunlukFiyatTL = table.Column<double>(nullable: false),
                    GunlukFiyatUSD = table.Column<double>(nullable: false),
                    GunlukFiyatEURO = table.Column<double>(nullable: false),
                    MikrotikIp = table.Column<string>(nullable: true),
                    MikrotikPort = table.Column<string>(nullable: true),
                    MikrotikUser = table.Column<string>(nullable: true),
                    MikrotikPass = table.Column<string>(nullable: true),
                    MikrotikDefaultSifre = table.Column<string>(nullable: true),
                    AdSoyadZorunlu = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_H_Ayarlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "H_Firmalar",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirmaId = table.Column<Guid>(nullable: false),
                    FirmaKodu = table.Column<int>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    FirmaAdi = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    YetkiliAdSoyad = table.Column<string>(nullable: true),
                    Aktif = table.Column<bool>(nullable: false),
                    BaslamaTarihi = table.Column<DateTime>(nullable: false),
                    BitisTarihi = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_H_Firmalar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "H_InternetSatis",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirmaId = table.Column<Guid>(nullable: false),
                    FirmaKodu = table.Column<int>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    Sifre = table.Column<string>(nullable: true),
                    AdSoyad = table.Column<string>(nullable: true),
                    VatNo = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    Odano = table.Column<string>(nullable: true),
                    Tutar = table.Column<double>(nullable: false),
                    AlinanTutar = table.Column<double>(nullable: false),
                    Doviz = table.Column<string>(nullable: true),
                    Gun = table.Column<int>(nullable: false),
                    BaslamaTarihi = table.Column<DateTime>(nullable: false),
                    BitisTarihi = table.Column<DateTime>(nullable: false),
                    Aktarildi = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_H_InternetSatis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "H_Kullanicilar",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirmaId = table.Column<Guid>(nullable: false),
                    FirmaKodu = table.Column<int>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    KullaniciKodu = table.Column<string>(nullable: true),
                    Sifre = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    Yetki = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_H_Kullanicilar", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "H_Ayarlar");

            migrationBuilder.DropTable(
                name: "H_Firmalar");

            migrationBuilder.DropTable(
                name: "H_InternetSatis");

            migrationBuilder.DropTable(
                name: "H_Kullanicilar");
        }
    }
}
