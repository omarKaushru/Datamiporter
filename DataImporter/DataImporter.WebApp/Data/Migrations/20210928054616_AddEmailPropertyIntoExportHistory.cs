using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.WebApp.Data.Migrations
{
    public partial class AddEmailPropertyIntoExportHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MailingAddress",
                table: "ExportHistories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MailingAddress",
                table: "ExportHistories");
        }
    }
}
