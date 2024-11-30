using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebJar.Backend.Migrations
{
    /// <inheritdoc />
    public partial class RelacionEmpresaCuenta : Migration
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
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
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
                name: "Cuentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false),
                    DebeHaber = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    Cargos = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    Abonos = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    SaldoMes = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    CargosMes = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    AbonosMes = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    SaldoCierre = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    CodigoMayor = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    CodigoPres = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    IngresoCash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EgresoCash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuentas_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_EmpresaId_Codigo",
                table: "Cuentas",
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
                name: "Cuentas");

            migrationBuilder.DropTable(
                name: "TiposConta");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
