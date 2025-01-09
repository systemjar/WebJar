using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebJar.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AgregamosElMesEnLaTablaPoliza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ElMes",
                table: "Polizas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElMes",
                table: "Polizas");
        }
    }
}