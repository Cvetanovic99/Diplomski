using Microsoft.EntityFrameworkCore.Migrations;

namespace Diplomski.Infrastructure.Persistence.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FileTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Type",
                value: "application/");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FileTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Type",
                value: "application");
        }
    }
}
