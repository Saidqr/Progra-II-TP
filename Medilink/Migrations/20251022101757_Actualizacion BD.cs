using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medilink.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Medicos_IdMedico",
                table: "Consultas");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Roles_IdRol",
                table: "Personas");

            migrationBuilder.DropTable(
                name: "HospitalMedico");

            migrationBuilder.DropTable(
                name: "Hospitales");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropIndex(
                name: "IX_Personas_IdRol",
                table: "Personas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Consultas",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IdRol",
                table: "Personas");

            migrationBuilder.RenameTable(
                name: "Consultas",
                newName: "ConsultasMedicas");

            migrationBuilder.RenameIndex(
                name: "IX_Consultas_IdMedico",
                table: "ConsultasMedicas",
                newName: "IX_ConsultasMedicas_IdMedico");

            migrationBuilder.AddColumn<string>(
                name: "Especialidad",
                table: "Roles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Expediente",
                table: "Roles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Matricula",
                table: "Roles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoRol",
                table: "Roles",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "Personas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DNI",
                table: "Personas",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Personas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NombreUsuario",
                table: "Personas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PassHash",
                table: "Personas",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaNacimiento",
                table: "Personas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "cantidadInventario",
                table: "Medicamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConsultasMedicas",
                table: "ConsultasMedicas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PersonasRoles",
                columns: table => new
                {
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonasRoles", x => new { x.PersonaId, x.RolId });
                    table.ForeignKey(
                        name: "FK_PersonasRoles_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonasRoles_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recetas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsultaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recetas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recetas_ConsultasMedicas_ConsultaId",
                        column: x => x.ConsultaId,
                        principalTable: "ConsultasMedicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultasMedicas_IdPaciente",
                table: "ConsultasMedicas",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_PersonasRoles_RolId",
                table: "PersonasRoles",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Recetas_ConsultaId",
                table: "Recetas",
                column: "ConsultaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultasMedicas_Roles_IdMedico",
                table: "ConsultasMedicas",
                column: "IdMedico",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultasMedicas_Roles_IdPaciente",
                table: "ConsultasMedicas",
                column: "IdPaciente",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultasMedicas_Roles_IdMedico",
                table: "ConsultasMedicas");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsultasMedicas_Roles_IdPaciente",
                table: "ConsultasMedicas");

            migrationBuilder.DropTable(
                name: "PersonasRoles");

            migrationBuilder.DropTable(
                name: "Recetas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConsultasMedicas",
                table: "ConsultasMedicas");

            migrationBuilder.DropIndex(
                name: "IX_ConsultasMedicas_IdPaciente",
                table: "ConsultasMedicas");

            migrationBuilder.DropColumn(
                name: "Especialidad",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Expediente",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Matricula",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "TipoRol",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "DNI",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "NombreUsuario",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "PassHash",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "fechaNacimiento",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "cantidadInventario",
                table: "Medicamentos");

            migrationBuilder.RenameTable(
                name: "ConsultasMedicas",
                newName: "Consultas");

            migrationBuilder.RenameIndex(
                name: "IX_ConsultasMedicas_IdMedico",
                table: "Consultas",
                newName: "IX_Consultas_IdMedico");

            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdRol",
                table: "Personas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Consultas",
                table: "Consultas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Hospitales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Especialidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Matricula = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicos_Roles_Id",
                        column: x => x.Id,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HospitalMedico",
                columns: table => new
                {
                    HospitalesId = table.Column<int>(type: "int", nullable: false),
                    MedicosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalMedico", x => new { x.HospitalesId, x.MedicosId });
                    table.ForeignKey(
                        name: "FK_HospitalMedico_Hospitales_HospitalesId",
                        column: x => x.HospitalesId,
                        principalTable: "Hospitales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HospitalMedico_Medicos_MedicosId",
                        column: x => x.MedicosId,
                        principalTable: "Medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personas_IdRol",
                table: "Personas",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalMedico_MedicosId",
                table: "HospitalMedico",
                column: "MedicosId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_Matricula",
                table: "Medicos",
                column: "Matricula",
                unique: true,
                filter: "[Matricula] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Medicos_IdMedico",
                table: "Consultas",
                column: "IdMedico",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Roles_IdRol",
                table: "Personas",
                column: "IdRol",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
