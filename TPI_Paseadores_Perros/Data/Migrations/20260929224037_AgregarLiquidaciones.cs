using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarLiquidaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Liquidaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaseadorId = table.Column<int>(type: "int", nullable: false),
                    FechaDesde = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaHasta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liquidaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Liquidaciones_Paseadores_PaseadorId",
                        column: x => x.PaseadorId,
                        principalTable: "Paseadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LiquidacionDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LiquidacionId = table.Column<int>(type: "int", nullable: false),
                    PaseoId = table.Column<int>(type: "int", nullable: false),
                    Importe = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiquidacionDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiquidacionDetalles_Liquidaciones_LiquidacionId",
                        column: x => x.LiquidacionId,
                        principalTable: "Liquidaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LiquidacionDetalles_Paseos_PaseoId",
                        column: x => x.PaseoId,
                        principalTable: "Paseos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LiquidacionDetalles_LiquidacionId",
                table: "LiquidacionDetalles",
                column: "LiquidacionId");

            migrationBuilder.CreateIndex(
                name: "IX_LiquidacionDetalles_PaseoId",
                table: "LiquidacionDetalles",
                column: "PaseoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Liquidaciones_PaseadorId",
                table: "Liquidaciones",
                column: "PaseadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LiquidacionDetalles");

            migrationBuilder.DropTable(
                name: "Liquidaciones");
        }
    }
}
