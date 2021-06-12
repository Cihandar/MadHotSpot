using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class AddHViewKasa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "H_ViewKasa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirmaId = table.Column<Guid>(nullable: false),
                    FirmaKodu = table.Column<int>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    Doviz = table.Column<string>(nullable: true),
                    Satis = table.Column<double>(nullable: false),
                    Iade = table.Column<double>(nullable: false),
                    Bakiye = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_H_ViewKasa", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "H_ViewKasa");
        }
    }
}
