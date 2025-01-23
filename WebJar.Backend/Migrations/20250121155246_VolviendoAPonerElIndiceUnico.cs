using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebJar.Backend.Migrations
{
    /// <inheritdoc />
    public partial class VolviendoAPonerElIndiceUnico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Polizas_EmpresaId",
                table: "Polizas");

            migrationBuilder.AlterColumn<string>(
                name: "ElMes",
                table: "Polizas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Polizas_EmpresaId_Documento_TipoId_ElMes",
                table: "Polizas",
                columns: new[] { "EmpresaId", "Documento", "TipoId", "ElMes" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Polizas_EmpresaId_Documento_TipoId_ElMes",
                table: "Polizas");

            migrationBuilder.AlterColumn<string>(
                name: "ElMes",
                table: "Polizas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Polizas_EmpresaId",
                table: "Polizas",
                column: "EmpresaId");
        }
    }
}
