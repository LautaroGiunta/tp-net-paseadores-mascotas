using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class HashearContrasenas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "Contrasena",
                value: "$2a$11$awijDt6KCL2HjFegQYT9veuBOQs5e73wuIPuemlA93B.t9LVoQA3y");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                column: "Contrasena",
                value: "$2a$11$myJNnok7ay2koW4KqjG4E..7zSv7OpNgX0EyC30h2.pi1KbnzlU1.");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3,
                column: "Contrasena",
                value: "$2a$11$DoRb5PcZZGgn/IP3.TgII.6kK9/FGHsfnvQlCYElPSCmJcWfOOsoG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "Contrasena",
                value: "Admin123");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                column: "Contrasena",
                value: "Dueno123");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3,
                column: "Contrasena",
                value: "Paseo123");
        }
    }
}
