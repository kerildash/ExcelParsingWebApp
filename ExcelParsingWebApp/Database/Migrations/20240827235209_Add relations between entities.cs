using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcelParsingWebApp.Database.Migrations
{
    /// <inheritdoc />
    public partial class Addrelationsbetweenentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "SheetId",
                table: "Accounts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                columns: new[] { "Id", "ClassId" });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SheetId",
                table: "Classes",
                column: "SheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ClassId",
                table: "Accounts",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Classes_ClassId",
                table: "Accounts",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Sheets_SheetId",
                table: "Classes",
                column: "SheetId",
                principalTable: "Sheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Classes_ClassId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Sheets_SheetId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_SheetId",
                table: "Classes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ClassId",
                table: "Accounts");

            migrationBuilder.AddColumn<Guid>(
                name: "SheetId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                columns: new[] { "Id", "SheetId" });
        }
    }
}
