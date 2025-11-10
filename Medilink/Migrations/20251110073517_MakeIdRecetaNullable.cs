using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medilink.Migrations
{
    /// <inheritdoc />
    public partial class MakeIdRecetaNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ConsultasMedicas_IdReceta",
                table: "ConsultasMedicas");

            migrationBuilder.AlterColumn<int>(
                name: "IdReceta",
                table: "ConsultasMedicas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultasMedicas_IdReceta",
                table: "ConsultasMedicas",
                column: "IdReceta",
                unique: true,
                filter: "[IdReceta] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ConsultasMedicas_IdReceta",
                table: "ConsultasMedicas");

            migrationBuilder.AlterColumn<int>(
                name: "IdReceta",
                table: "ConsultasMedicas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConsultasMedicas_IdReceta",
                table: "ConsultasMedicas",
                column: "IdReceta",
                unique: true);
        }
    }
}
