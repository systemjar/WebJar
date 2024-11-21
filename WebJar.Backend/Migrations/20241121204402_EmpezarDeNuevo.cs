﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebJar.Backend.Migrations
{
    /// <inheritdoc />
    public partial class EmpezarDeNuevo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nit = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false),
                    Patrono = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false),
                    DirecionPatrono = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false),
                    NumeroPatronal = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Produce = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Nivel1 = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Nivel2 = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Nivel3 = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Nivel4 = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Nivel5 = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Nivel6 = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Activo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Pasivo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Capital = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Ventas = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Costos = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Gastos = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    OtrosIngresos = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    OtrosGastos = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Produccion = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    PorcentajeIva = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposConta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposConta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cuenta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false),
                    DebeHaber = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Cargos = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Abonos = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SaldoMes = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CargosMes = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AbonosMes = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SaldoCierre = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CodigoMayor = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    CodigoPres = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    IngresoCash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EgresoCash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuenta_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DefPolizas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuentaID = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefPolizas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DefPolizas_Cuenta_CuentaID",
                        column: x => x.CuentaID,
                        principalTable: "Cuenta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DefPolizas_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_EmpresaId_Codigo",
                table: "Cuenta",
                columns: new[] { "EmpresaId", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DefPolizas_CuentaID",
                table: "DefPolizas",
                column: "CuentaID");

            migrationBuilder.CreateIndex(
                name: "IX_DefPolizas_EmpresaId_Codigo",
                table: "DefPolizas",
                columns: new[] { "EmpresaId", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_Nit",
                table: "Empresas",
                column: "Nit",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TiposConta_Nombre",
                table: "TiposConta",
                column: "Nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefPolizas");

            migrationBuilder.DropTable(
                name: "TiposConta");

            migrationBuilder.DropTable(
                name: "Cuenta");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
