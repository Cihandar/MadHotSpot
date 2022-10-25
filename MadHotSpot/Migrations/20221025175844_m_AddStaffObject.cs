using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class m_AddStaffObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "H_Staffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirmaId = table.Column<Guid>(nullable: false),
                    FirmaKodu = table.Column<int>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Day = table.Column<string>(nullable: true),
                    Month = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    LastMac = table.Column<string>(nullable: true),
                    IdNumber = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    UserProfile = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    MikrotikId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_H_Staffs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "H_Staffs");
        }
    }
}
