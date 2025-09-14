using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QABS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cascade1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Enrollments_EnrollmentId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPayments_Enrollments_EnrollmentId",
                table: "StudentPayments");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Enrollments_EnrollmentId",
                table: "Sessions",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPayments_Enrollments_EnrollmentId",
                table: "StudentPayments",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Enrollments_EnrollmentId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPayments_Enrollments_EnrollmentId",
                table: "StudentPayments");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Enrollments_EnrollmentId",
                table: "Sessions",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPayments_Enrollments_EnrollmentId",
                table: "StudentPayments",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id");
        }
    }
}
