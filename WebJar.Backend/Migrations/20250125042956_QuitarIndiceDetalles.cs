using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebJar.Backend.Migrations
{
    /// <inheritdoc />
    public partial class QuitarIndiceDetalles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Detalles_PolizaId_Codigo",
                table: "Detalles");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_PolizaId",
                table: "Detalles",
                column: "PolizaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Detalles_PolizaId",
                table: "Detalles");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_PolizaId_Codigo",
                table: "Detalles",
                columns: new[] { "PolizaId", "Codigo" });
        }
    }
}
