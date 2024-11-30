using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebJar.Backend.Migrations
{
    /// <inheritdoc />
    public partial class EmpresaEntityCompleta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Activo",
                table: "Empresas",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Capital",
                table: "Empresas",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Costos",
                table: "Empresas",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Empresas",
                type: "nvarchar(65)",
                maxLength: 65,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DirecionPatrono",
                table: "Empresas",
                type: "nvarchar(65)",
                maxLength: 65,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gastos",
                table: "Empresas",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nivel1",
                table: "Empresas",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nivel2",
                table: "Empresas",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nivel3",
                table: "Empresas",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nivel4",
                table: "Empresas",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nivel5",
                table: "Empresas",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nivel6",
                table: "Empresas",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NivelPresupuesto",
                table: "Empresas",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NumeroPatronal",
                table: "Empresas",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OtrosGastos",
                table: "Empresas",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OtrosIngresos",
                table: "Empresas",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pasivo",
                table: "Empresas",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Patrono",
                table: "Empresas",
                type: "nvarchar(65)",
                maxLength: 65,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PorcentajeIva",
                table: "Empresas",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Produccion",
                table: "Empresas",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Produce",
                table: "Empresas",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Resultado",
                table: "Empresas",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ventas",
                table: "Empresas",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Capital",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Costos",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "DirecionPatrono",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Gastos",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Nivel1",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Nivel2",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Nivel3",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Nivel4",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Nivel5",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Nivel6",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "NivelPresupuesto",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "NumeroPatronal",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "OtrosGastos",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "OtrosIngresos",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Pasivo",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Patrono",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "PorcentajeIva",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Produccion",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Produce",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Resultado",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Ventas",
                table: "Empresas");
        }
    }
}
