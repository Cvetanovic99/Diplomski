using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Diplomski.Infrastructure.Persistence.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileTypes_Users_BelongsToId",
                table: "FileTypes");

            migrationBuilder.DropIndex(
                name: "IX_FileTypes_BelongsToId",
                table: "FileTypes");

            migrationBuilder.DropColumn(
                name: "BelongsToId",
                table: "FileTypes");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "FileTypes");

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
                    UserId = table.Column<int>(type: "int", nullable: true),
                    FileTypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "BelongsToId",
                table: "FileTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "FileTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FileTypes_BelongsToId",
                table: "FileTypes",
                column: "BelongsToId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileTypes_Users_BelongsToId",
                table: "FileTypes",
                column: "BelongsToId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
