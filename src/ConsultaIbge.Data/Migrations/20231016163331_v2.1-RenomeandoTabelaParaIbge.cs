using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsultaIbge.Data.Migrations
{
    /// <inheritdoc />
    public partial class v21RenomeandoTabelaParaIbge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_localidade",
                table: "localidade");

            migrationBuilder.RenameTable(
                name: "localidade",
                newName: "ibge");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ibge",
                table: "ibge",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ibge",
                table: "ibge");

            migrationBuilder.RenameTable(
                name: "ibge",
                newName: "localidade");

            migrationBuilder.AddPrimaryKey(
                name: "PK_localidade",
                table: "localidade",
                column: "Id");
        }
    }
}
