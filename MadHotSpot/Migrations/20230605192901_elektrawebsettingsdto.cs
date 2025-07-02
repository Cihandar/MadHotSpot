using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class elektrawebsettingsdto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.AddColumn<int>(
                name: "LoginType",
                table: "H_CustomerInfo",
                nullable: false,
                defaultValue: 0);
 

            migrationBuilder.CreateTable(
                name: "H_ElektraWebSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirmaId = table.Column<Guid>(nullable: false),
                    FirmaKodu = table.Column<int>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    HotelId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: false),
                    GelirGrubuId = table.Column<int>(nullable: false),
                    CashDepartmentId = table.Column<int>(nullable: false),
                    CreditDepartmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_H_ElektraWebSetting", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "H_ElektraWebSetting");

            migrationBuilder.DropColumn(
                name: "LoginType",
                table: "H_CustomerInfo");

            migrationBuilder.DropColumn(
                name: "ElektraEntegrasyonAktif",
                table: "H_Ayarlar");

            migrationBuilder.DropColumn(
                name: "ElektraPassword",
                table: "H_Ayarlar");

            migrationBuilder.DropColumn(
                name: "ElektraTenantId",
                table: "H_Ayarlar");

            migrationBuilder.DropColumn(
                name: "ElektraUser",
                table: "H_Ayarlar");

            migrationBuilder.DropColumn(
                name: "ManuelGuestAdd",
                table: "H_Ayarlar");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "H_CustomerInfo",
                nullable: true);
        }
    }
}
