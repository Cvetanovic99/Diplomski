using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Diplomski.Infrastructure.Persistence.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_FileTypes_TypeId",
                table: "Files");

            migrationBuilder.DropTable(
                name: "UserFileTypes");

            migrationBuilder.DropIndex(
                name: "IX_Files_TypeId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Files");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "FileTypes",
                columns: new[] { "Id", "CreatedAt", "Type", "UpdatedAt" },
                values: new object[] { 1, null, "image/", null });

            migrationBuilder.InsertData(
                table: "FileTypes",
                columns: new[] { "Id", "CreatedAt", "Type", "UpdatedAt" },
                values: new object[] { 2, null, "text/", null });

            migrationBuilder.InsertData(
                table: "FileTypes",
                columns: new[] { "Id", "CreatedAt", "Type", "UpdatedAt" },
                values: new object[] { 3, null, "application", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FileTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FileTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FileTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Files");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Files",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserFileTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileTypeId = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFileTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFileTypes_FileTypes_FileTypeId",
                        column: x => x.FileTypeId,
                        principalTable: "FileTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFileTypes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_TypeId",
                table: "Files",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFileTypes_FileTypeId",
                table: "UserFileTypes",
                column: "FileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFileTypes_UserId",
                table: "UserFileTypes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_FileTypes_TypeId",
                table: "Files",
                column: "TypeId",
                principalTable: "FileTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
