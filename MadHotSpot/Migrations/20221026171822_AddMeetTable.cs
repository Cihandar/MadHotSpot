using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MadHotSpot.Migrations
{
    public partial class AddMeetTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "H_Meets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirmaId = table.Column<Guid>(nullable: false),
                    FirmaKodu = table.Column<int>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AccessLimit = table.Column<string>(nullable: true),
                    RateLimit = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    PasswordExpire = table.Column<DateTime>(nullable: false),
                    MikrotikId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_H_Meets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "H_Meets");
        }
    }
}
