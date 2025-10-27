using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medilink.Migrations
{
    /// <inheritdoc />
    public partial class ConsultaReceta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recetas_ConsultasMedicas_ConsultaId",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_Recetas_ConsultaId",
                table: "Recetas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicamentos",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "ConsultaId",
                table: "Recetas");

            migrationBuilder.RenameTable(
                name: "Medicamentos",
                newName: "Insumos");

            migrationBuilder.AddColumn<int>(
                name: "IdReceta",
                table: "ConsultasMedicas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaVencimiento",
                table: "Insumos",
                type: "datetime2",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFabricacion",
                table: "Insumos",
                type: "datetime2",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 12);

            migrationBuilder.AddColumn<string>(
                name: "TipoInsumo",
                table: "Insumos",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Insumos",
                table: "Insumos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RecetaMedicamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdReceta = table.Column<int>(type: "int", nullable: false),
                    IdMedicamento = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaMedicamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecetaMedicamentos_Insumos_IdMedicamento",
                        column: x => x.IdMedicamento,
                        principalTable: "Insumos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecetaMedicamentos_Recetas_IdReceta",
                        column: x => x.IdReceta,
                        principalTable: "Recetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultasMedicas_IdReceta",
                table: "ConsultasMedicas",
                column: "IdReceta",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecetaMedicamentos_IdMedicamento",
                table: "RecetaMedicamentos",
                column: "IdMedicamento");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaMedicamentos_IdReceta",
                table: "RecetaMedicamentos",
                column: "IdReceta");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultasMedicas_Recetas_IdReceta",
                table: "ConsultasMedicas",
                column: "IdReceta",
                principalTable: "Recetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultasMedicas_Recetas_IdReceta",
                table: "ConsultasMedicas");

            migrationBuilder.DropTable(
                name: "RecetaMedicamentos");

            migrationBuilder.DropIndex(
                name: "IX_ConsultasMedicas_IdReceta",
                table: "ConsultasMedicas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Insumos",
                table: "Insumos");

            migrationBuilder.DropColumn(
                name: "IdReceta",
                table: "ConsultasMedicas");

            migrationBuilder.DropColumn(
                name: "TipoInsumo",
                table: "Insumos");

            migrationBuilder.RenameTable(
                name: "Insumos",
                newName: "Medicamentos");

            migrationBuilder.AddColumn<int>(
                name: "ConsultaId",
                table: "Recetas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaVencimiento",
                table: "Medicamentos",
                type: "datetime2",
                maxLength: 12,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFabricacion",
                table: "Medicamentos",
                type: "datetime2",
                maxLength: 12,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicamentos",
                table: "Medicamentos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Recetas_ConsultaId",
                table: "Recetas",
                column: "ConsultaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recetas_ConsultasMedicas_ConsultaId",
                table: "Recetas",
                column: "ConsultaId",
                principalTable: "ConsultasMedicas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
