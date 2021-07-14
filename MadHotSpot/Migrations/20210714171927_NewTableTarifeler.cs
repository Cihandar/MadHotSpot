using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class NewTableTarifeler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "H_Tarifeler",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirmaId = table.Column<Guid>(nullable: false),
                    FirmaKodu = table.Column<int>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    TarifeAdi = table.Column<string>(nullable: true),
                    Gun = table.Column<int>(nullable: false),
                    Fiyat = table.Column<double>(nullable: false),
                    Doviz = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_H_Tarifeler", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "H_Tarifeler");
        }
    }
}
