using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.WebApp.Data.Migrations
{
    public partial class AddGroupIdInExcelFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "ExcelFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "ExcelFiles");
        }
    }
}
