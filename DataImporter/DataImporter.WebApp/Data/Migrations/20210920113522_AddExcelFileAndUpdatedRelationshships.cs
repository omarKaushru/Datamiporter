using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.WebApp.Data.Migrations
{
    public partial class AddExcelFileAndUpdatedRelationshships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExcelDatas_ExcelRecords_CurrentExcelRecordId",
                table: "ExcelDatas");

            migrationBuilder.RenameColumn(
                name: "CurrentExcelRecordId",
                table: "ExcelDatas",
                newName: "ExcelRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_ExcelDatas_CurrentExcelRecordId",
                table: "ExcelDatas",
                newName: "IX_ExcelDatas_ExcelRecordId");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "ExcelRecords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ExcelFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelFiles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExcelRecords_ApplicationUserId",
                table: "ExcelRecords",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExcelDatas_ExcelRecords_ExcelRecordId",
                table: "ExcelDatas",
                column: "ExcelRecordId",
                principalTable: "ExcelRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExcelRecords_AspNetUsers_ApplicationUserId",
                table: "ExcelRecords",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExcelDatas_ExcelRecords_ExcelRecordId",
                table: "ExcelDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_ExcelRecords_AspNetUsers_ApplicationUserId",
                table: "ExcelRecords");

            migrationBuilder.DropTable(
                name: "ExcelFiles");

            migrationBuilder.DropIndex(
                name: "IX_ExcelRecords_ApplicationUserId",
                table: "ExcelRecords");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ExcelRecords");

            migrationBuilder.RenameColumn(
                name: "ExcelRecordId",
                table: "ExcelDatas",
                newName: "CurrentExcelRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_ExcelDatas_ExcelRecordId",
                table: "ExcelDatas",
                newName: "IX_ExcelDatas_CurrentExcelRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExcelDatas_ExcelRecords_CurrentExcelRecordId",
                table: "ExcelDatas",
                column: "CurrentExcelRecordId",
                principalTable: "ExcelRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
