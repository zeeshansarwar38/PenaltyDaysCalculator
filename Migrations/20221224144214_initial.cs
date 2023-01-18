using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PenaltyCalc.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountrySettings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    countryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    weekendDays = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    currencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    penaltyAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountrySettings", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountrySettings");
        }
    }
}
