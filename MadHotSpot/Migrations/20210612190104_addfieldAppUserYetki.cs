using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class addfieldAppUserYetki : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sifre",
                table: "H_Kullanicilar");

            migrationBuilder.RenameColumn(
                name: "Telefon",
                table: "H_Kullanicilar",
                newName: "Password");

            migrationBuilder.AlterColumn<int>(
                name: "Yetki",
                table: "H_Kullanicilar",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Yetki",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Yetki",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "H_Kullanicilar",
                newName: "Telefon");

            migrationBuilder.AlterColumn<string>(
                name: "Yetki",
                table: "H_Kullanicilar",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Sifre",
                table: "H_Kullanicilar",
                nullable: true);
        }
    }
}
