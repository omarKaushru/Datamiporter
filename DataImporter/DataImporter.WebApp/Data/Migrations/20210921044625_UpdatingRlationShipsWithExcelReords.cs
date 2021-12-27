using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.WebApp.Data.Migrations
{
    public partial class UpdatingRlationShipsWithExcelReords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExcelRecords_AspNetUsers_ApplicationUserId",
                table: "ExcelRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ExcelRecords_Groups_GroupsId",
                table: "ExcelRecords");

            migrationBuilder.DropIndex(
                name: "IX_ExcelRecords_ApplicationUserId",
                table: "ExcelRecords");

            migrationBuilder.DropIndex(
                name: "IX_ExcelRecords_GroupsId",
                table: "ExcelRecords");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupsId",
                table: "ExcelRecords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "GroupsId",
                table: "ExcelRecords",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_ExcelRecords_ApplicationUserId",
                table: "ExcelRecords",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExcelRecords_GroupsId",
                table: "ExcelRecords",
                column: "GroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExcelRecords_AspNetUsers_ApplicationUserId",
                table: "ExcelRecords",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExcelRecords_Groups_GroupsId",
                table: "ExcelRecords",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
