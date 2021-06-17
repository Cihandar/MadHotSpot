using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class UpdateInterSatisAddIadeTurleri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IadeEdilenDoviz",
                table: "H_InternetSatis",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "IadeEdilenTutar",
                table: "H_InternetSatis",
                nullable: false,
                defaultValue: 0.0);
 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "H_ViewKasa");

            migrationBuilder.DropColumn(
                name: "IadeEdilenDoviz",
                table: "H_InternetSatis");

            migrationBuilder.DropColumn(
                name: "IadeEdilenTutar",
                table: "H_InternetSatis");
        }
    }
}
