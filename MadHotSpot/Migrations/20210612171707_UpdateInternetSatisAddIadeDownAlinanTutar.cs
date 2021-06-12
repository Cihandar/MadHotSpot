using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class UpdateInternetSatisAddIadeDownAlinanTutar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlinanTutar",
                table: "H_InternetSatis");

            migrationBuilder.AddColumn<bool>(
                name: "Iade",
                table: "H_InternetSatis",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iade",
                table: "H_InternetSatis");

            migrationBuilder.AddColumn<double>(
                name: "AlinanTutar",
                table: "H_InternetSatis",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
