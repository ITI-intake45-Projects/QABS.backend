using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QABS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deleteTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherAvailabilities_Teachers_TeacherId",
                table: "TeacherAvailabilities");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherAvailabilities_Teachers_TeacherId",
                table: "TeacherAvailabilities",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherAvailabilities_Teachers_TeacherId",
                table: "TeacherAvailabilities");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherAvailabilities_Teachers_TeacherId",
                table: "TeacherAvailabilities",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
