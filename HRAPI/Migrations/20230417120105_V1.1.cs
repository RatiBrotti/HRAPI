using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRAPI.Migrations
{
    /// <inheritdoc />
    public partial class V11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrator_Emploee_EmployeeId",
                table: "Administrator");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrator_Emploee_EmployeeId",
                table: "Administrator",
                column: "EmployeeId",
                principalTable: "Emploee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrator_Emploee_EmployeeId",
                table: "Administrator");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrator_Emploee_EmployeeId",
                table: "Administrator",
                column: "EmployeeId",
                principalTable: "Emploee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
