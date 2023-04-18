using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRAPI.Migrations
{
    /// <inheritdoc />
    public partial class v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Emploee_AdministratorId",
                table: "Emploee");

            migrationBuilder.AlterColumn<int>(
                name: "AdministratorId",
                table: "Emploee",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Emploee_AdministratorId",
                table: "Emploee",
                column: "AdministratorId",
                unique: true,
                filter: "[AdministratorId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Emploee_AdministratorId",
                table: "Emploee");

            migrationBuilder.AlterColumn<int>(
                name: "AdministratorId",
                table: "Emploee",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emploee_AdministratorId",
                table: "Emploee",
                column: "AdministratorId",
                unique: true);
        }
    }
}
