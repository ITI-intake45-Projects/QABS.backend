using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QABS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateStudentConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPayments_Students_StudentId",
                table: "StudentPayments");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPayments_Students_StudentId",
                table: "StudentPayments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPayments_Students_StudentId",
                table: "StudentPayments");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPayments_Students_StudentId",
                table: "StudentPayments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId");
        }
    }
}
