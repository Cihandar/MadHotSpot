using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class AddRezervasyonlar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "H_Rezervasyonlar",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirmaId = table.Column<Guid>(nullable: false),
                    FirmaKodu = table.Column<int>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    AyrilisTarihi = table.Column<DateTime>(nullable: false),
                    KimlikNo = table.Column<string>(nullable: true),
                    Tcno = table.Column<string>(nullable: true),
                    DogumTarihi = table.Column<DateTime>(nullable: false),
                    Odano = table.Column<string>(nullable: true),
                    MusteriAdi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_H_Rezervasyonlar", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "H_Rezervasyonlar");
        }
    }
}
