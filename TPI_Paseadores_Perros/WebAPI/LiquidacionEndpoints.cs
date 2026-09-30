using Application.Services;
using DTOs;

namespace WebAPI
{
    // Liquidaciones a paseadores (Maestro/Detalle). Es plata: todo lo opera solo el Admin.
    public static class LiquidacionEndpoints
    {
        public static void MapLiquidacionEndpoints(this WebApplication app)
        {
            app.MapGet("/liquidaciones/{id}", async (int id, ILiquidacionService liquidacionService) =>
            {
                LiquidacionDTO? dto = await liquidacionService.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetLiquidacion")
            .RequireAuthorization(Politicas.SoloAdmin)
            .Produces<LiquidacionDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/liquidaciones", async (ILiquidacionService liquidacionService) =>
            {
                var dtos = await liquidacionService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllLiquidaciones")
            .RequireAuthorization(Politicas.SoloAdmin)
            .Produces<List<LiquidacionDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            // Los paseos que se pueden agregar como líneas: terminados y sin liquidar
            app.MapGet("/liquidaciones/paseos-pendientes", async (int paseadorId, DateTime fechaDesde, DateTime fechaHasta,
                                                                  ILiquidacionService liquidacionService) =>
            {
                try
                {
                    var paseos = await liquidacionService.GetPaseosPendientesAsync(paseadorId, fechaDesde, fechaHasta);
                    return Results.Ok(paseos);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("GetPaseosPendientesDeLiquidar")
            .RequireAuthorization(Politicas.SoloAdmin)
            .Produces<List<PaseoDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            // Cabecera y detalle viajan juntos y se guardan en una sola operación
            app.MapPost("/liquidaciones", async (LiquidacionDTO dto, ILiquidacionService liquidacionService) =>
            {
                try
                {
                    LiquidacionDTO liquidacionDTO = await liquidacionService.AddAsync(dto);
                    return Results.Created($"/liquidaciones/{liquidacionDTO.Id}", liquidacionDTO);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddLiquidacion")
            .RequireAuthorization(Politicas.SoloAdmin)
            .Produces<LiquidacionDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/liquidaciones", async (LiquidacionDTO dto, ILiquidacionService liquidacionService) =>
            {
                try
                {
                    var found = await liquidacionService.UpdateAsync(dto);

                    if (!found)
                    {
                        return Results.NotFound();
                    }

                    return Results.NoContent();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdateLiquidacion")
            .RequireAuthorization(Politicas.SoloAdmin)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/liquidaciones/{id}", async (int id, ILiquidacionService liquidacionService) =>
            {
                var deleted = await liquidacionService.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeleteLiquidacion")
            .RequireAuthorization(Politicas.SoloAdmin)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
