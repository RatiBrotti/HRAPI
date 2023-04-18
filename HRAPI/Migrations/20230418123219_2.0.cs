using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRAPI.Migrations
{
    /// <inheritdoc />
    public partial class _20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emploee_Administrator_AdministratorId",
                table: "Emploee");

            migrationBuilder.AlterColumn<int>(
                name: "AdministratorId",
                table: "Emploee",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Emploee_Administrator_AdministratorId",
                table: "Emploee",
                column: "AdministratorId",
                principalTable: "Administrator",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emploee_Administrator_AdministratorId",
                table: "Emploee");

            migrationBuilder.AlterColumn<int>(
                name: "AdministratorId",
                table: "Emploee",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Emploee_Administrator_AdministratorId",
                table: "Emploee",
                column: "AdministratorId",
                principalTable: "Administrator",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
