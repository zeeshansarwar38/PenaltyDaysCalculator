using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PenaltyCalc.Migrations
{
    public partial class addholiday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "otherHolidays",
                table: "CountrySettings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "otherHolidays",
                table: "CountrySettings");
        }
    }
}
