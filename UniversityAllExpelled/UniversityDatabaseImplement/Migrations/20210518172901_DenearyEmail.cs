using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityDatabaseImplement.Migrations
{
    public partial class DenearyEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Denearies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Denearies");
        }
    }
}
