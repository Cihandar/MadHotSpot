using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class AddHViewKasa2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Tarih",
                table: "H_ViewKasa",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tarih",
                table: "H_ViewKasa");
        }
    }
}
