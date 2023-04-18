using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRAPI.Migrations
{
    /// <inheritdoc />
    public partial class V18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emploee_Administrator_AdministratorId",
                table: "Emploee");

            migrationBuilder.AddForeignKey(
                name: "FK_Emploee_Administrator_AdministratorId",
                table: "Emploee",
                column: "AdministratorId",
                principalTable: "Administrator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emploee_Administrator_AdministratorId",
                table: "Emploee");

            migrationBuilder.AddForeignKey(
                name: "FK_Emploee_Administrator_AdministratorId",
                table: "Emploee",
                column: "AdministratorId",
                principalTable: "Administrator",
                principalColumn: "Id");
        }
    }
}
