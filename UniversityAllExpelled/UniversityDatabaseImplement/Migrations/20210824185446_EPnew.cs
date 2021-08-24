using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityDatabaseImplement.Migrations
{
    public partial class EPnew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StreamName",
                table: "EducationPlans");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "EducationPlans",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "EducationPlans",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EducationPlans",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "EducationPlans");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "EducationPlans");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "EducationPlans");

            migrationBuilder.AddColumn<string>(
                name: "StreamName",
                table: "EducationPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
