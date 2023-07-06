using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PdfGenerator.Infrastructure.Migrations
{
    public partial class updating_additional_context : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AditionalContext",
                table: "HtmlTemplates",
                newName: "AdditionalContext");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdditionalContext",
                table: "HtmlTemplates",
                newName: "AditionalContext");
        }
    }
}
