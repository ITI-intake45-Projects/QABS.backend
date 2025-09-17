using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QABS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deleteTeacher3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Teachers_TeacherId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherAvailabilities_Teachers_TeacherId",
                table: "TeacherAvailabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherPayouts_Teachers_TeacherId",
                table: "TeacherPayouts");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Teachers_TeacherId",
                table: "Enrollments",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherAvailabilities_Teachers_TeacherId",
                table: "TeacherAvailabilities",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherPayouts_Teachers_TeacherId",
                table: "TeacherPayouts",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Teachers_TeacherId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherAvailabilities_Teachers_TeacherId",
                table: "TeacherAvailabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherPayouts_Teachers_TeacherId",
                table: "TeacherPayouts");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Teachers_TeacherId",
                table: "Enrollments",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherAvailabilities_Teachers_TeacherId",
                table: "TeacherAvailabilities",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherPayouts_Teachers_TeacherId",
                table: "TeacherPayouts",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "UserId");
        }
    }
}
