using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsultaIbge.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(7)", nullable: false),
                    Name = table.Column<string>(type: "varchar(80)", nullable: false),
                    Email = table.Column<string>(type: "varchar(80)", nullable: false),
                    Password = table.Column<string>(type: "varchar(80)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
