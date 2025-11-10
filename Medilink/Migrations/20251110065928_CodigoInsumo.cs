using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medilink.Migrations
{
    /// <inheritdoc />
    public partial class CodigoInsumo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonasRoles_Personas_PersonaId",
                table: "PersonasRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonasRoles_Roles_RolId",
                table: "PersonasRoles");

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Insumos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonasRoles_Personas_PersonaId",
                table: "PersonasRoles",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonasRoles_Roles_RolId",
                table: "PersonasRoles",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonasRoles_Personas_PersonaId",
                table: "PersonasRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonasRoles_Roles_RolId",
                table: "PersonasRoles");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Insumos");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonasRoles_Personas_PersonaId",
                table: "PersonasRoles",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonasRoles_Roles_RolId",
                table: "PersonasRoles",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
