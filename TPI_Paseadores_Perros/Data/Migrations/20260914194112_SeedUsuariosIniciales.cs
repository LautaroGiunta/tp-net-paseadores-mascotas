using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsuariosIniciales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Apellido", "Contrasena", "Email", "FechaAlta", "Nombre", "Rol", "Telefono" },
                values: new object[,]
                {
                    { 1, "Sistema", "Admin123", "admin@paseos.com", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, "3415550100" },
                    { 2, "Gomez", "Dueno123", "ana.gomez@paseos.com", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ana", 1, "3415550101" },
                    { 3, "Ruiz", "Paseo123", "carlos.ruiz@paseos.com", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carlos", 2, "3415550102" }
                });

            migrationBuilder.InsertData(
                table: "Duenos",
                columns: new[] { "Id", "Direccion" },
                values: new object[] { 2, "Sarmiento 1234" });

            migrationBuilder.InsertData(
                table: "Paseadores",
                columns: new[] { "Id", "TarifaPorHora", "Zona" },
                values: new object[] { 3, 3500m, "Centro" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Duenos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Paseadores",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
