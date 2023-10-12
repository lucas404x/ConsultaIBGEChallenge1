using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsultaIbge.Data.Migrations
{
    /// <inheritdoc />
    public partial class v1Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ibge",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(7)", nullable: false),
                    State = table.Column<string>(type: "char(2)", nullable: false),
                    City = table.Column<string>(type: "varchar(80)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ibge", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IBGE_City",
                table: "ibge",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_IBGE_Id",
                table: "ibge",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_IBGE_State",
                table: "ibge",
                column: "State");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ibge");
        }
    }
}
