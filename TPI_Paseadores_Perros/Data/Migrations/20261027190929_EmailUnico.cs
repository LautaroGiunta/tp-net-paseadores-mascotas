using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class EmailUnico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Si la base ya tiene emails repetidos, el índice no se puede crear y la API no arranca.
            // Se queda con su email el de menor Id, que era el que encontraba el login, y a los
            // demás se les antepone "repetido{Id}." para que el admin les cargue un email nuevo.
            migrationBuilder.Sql(@"
                WITH Repetidos AS (
                    SELECT Id, ROW_NUMBER() OVER (PARTITION BY Email ORDER BY Id) AS Orden
                    FROM Usuarios
                )
                UPDATE u
                SET Email = CONCAT('repetido', u.Id, '.', u.Email)
                FROM Usuarios u
                INNER JOIN Repetidos r ON r.Id = u.Id
                WHERE r.Orden > 1;");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuarios",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Los emails renombrados en Up se dejan como están
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);
        }
    }
}
