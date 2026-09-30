using Application.Services;
using DTOs;

namespace WebAPI
{
    // Reportes: muestran plata de todos los paseadores y dueños, así que son solo del Admin
    public static class ReporteEndpoints
    {
        public static void MapReporteEndpoints(this WebApplication app)
        {
            app.MapGet("/reportes/recaudacion-mensual", async (DateTime fechaDesde, DateTime fechaHasta,
                                                               IReporteService reporteService) =>
            {
                try
                {
                    var filas = await reporteService.GetRecaudacionMensualAsync(fechaDesde, fechaHasta);
                    return Results.Ok(filas);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("GetRecaudacionMensual")
            .RequireAuthorization(Politicas.SoloAdmin)
            .Produces<List<RecaudacionMensualDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapGet("/reportes/actividad-perros", async (DateTime fechaDesde, DateTime fechaHasta,
                                                            IReporteService reporteService) =>
            {
                try
                {
                    var filas = await reporteService.GetActividadPerrosAsync(fechaDesde, fechaHasta);
                    return Results.Ok(filas);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("GetActividadPerros")
            .RequireAuthorization(Politicas.SoloAdmin)
            .Produces<List<ActividadPerroDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();
        }
    }
}
