using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class mig_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocalIp",
                table: "H_Logs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mac",
                table: "H_Logs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalIp",
                table: "H_Logs");

            migrationBuilder.DropColumn(
                name: "Mac",
                table: "H_Logs");
        }
    }
}
