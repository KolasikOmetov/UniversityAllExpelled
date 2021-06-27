using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityDatabaseImplement.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlanStudents_Students_GradebookNumber",
                table: "EducationPlanStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Students_GradebookNumber",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_GradebookNumber",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_EducationPlanStudents_GradebookNumber",
                table: "EducationPlanStudents");

            migrationBuilder.DropColumn(
                name: "GradebookNumber",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "GradebookNumber",
                table: "EducationPlanStudents");

            migrationBuilder.AddColumn<string>(
                name: "StudentGradebookNumber",
                table: "StudentSubjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentGradebookNumber",
                table: "EducationPlanStudents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_StudentGradebookNumber",
                table: "StudentSubjects",
                column: "StudentGradebookNumber");

            migrationBuilder.CreateIndex(
                name: "IX_EducationPlanStudents_StudentGradebookNumber",
                table: "EducationPlanStudents",
                column: "StudentGradebookNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlanStudents_Students_StudentGradebookNumber",
                table: "EducationPlanStudents",
                column: "StudentGradebookNumber",
                principalTable: "Students",
                principalColumn: "GradebookNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Students_StudentGradebookNumber",
                table: "StudentSubjects",
                column: "StudentGradebookNumber",
                principalTable: "Students",
                principalColumn: "GradebookNumber",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlanStudents_Students_StudentGradebookNumber",
                table: "EducationPlanStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Students_StudentGradebookNumber",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_StudentGradebookNumber",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_EducationPlanStudents_StudentGradebookNumber",
                table: "EducationPlanStudents");

            migrationBuilder.DropColumn(
                name: "StudentGradebookNumber",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "StudentGradebookNumber",
                table: "EducationPlanStudents");

            migrationBuilder.AddColumn<string>(
                name: "GradebookNumber",
                table: "StudentSubjects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GradebookNumber",
                table: "EducationPlanStudents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_GradebookNumber",
                table: "StudentSubjects",
                column: "GradebookNumber");

            migrationBuilder.CreateIndex(
                name: "IX_EducationPlanStudents_GradebookNumber",
                table: "EducationPlanStudents",
                column: "GradebookNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlanStudents_Students_GradebookNumber",
                table: "EducationPlanStudents",
                column: "GradebookNumber",
                principalTable: "Students",
                principalColumn: "GradebookNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Students_GradebookNumber",
                table: "StudentSubjects",
                column: "GradebookNumber",
                principalTable: "Students",
                principalColumn: "GradebookNumber",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
