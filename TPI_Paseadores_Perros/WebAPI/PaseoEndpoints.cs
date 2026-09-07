using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class PaseoEndpoints
    {
        public static void MapPaseoEndpoints(this WebApplication app)
        {
            app.MapGet("/paseos/{id}", async (int id, IPaseoService paseoService) =>
            {
                PaseoDTO? dto = await paseoService.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetPaseo")
            .Produces<PaseoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/paseos", async (IPaseoService paseoService) =>
            {
                var dtos = await paseoService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllPaseos")
            .Produces<List<PaseoDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/paseos/buscar", async (int? paseadorId, int? perroId, DateTime? fechaDesde,
                                                DateTime? fechaHasta, IPaseoService paseoService) =>
            {
                try
                {
                    var criteria = new PaseoCriteriaDTO
                    {
                        PaseadorId = paseadorId,
                        PerroId = perroId,
                        FechaDesde = fechaDesde,
                        FechaHasta = fechaHasta
                    };
                    var paseos = await paseoService.GetByCriteriaAsync(criteria);
                    return Results.Ok(paseos);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("GetPaseosByCriteria")
            .Produces<List<PaseoDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPost("/paseos", async (PaseoDTO dto, IPaseoService paseoService) =>
            {
                try
                {
                    PaseoDTO paseoDTO = await paseoService.AddAsync(dto);
                    return Results.Created($"/paseos/{paseoDTO.Id}", paseoDTO);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddPaseo")
            .Produces<PaseoDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/paseos", async (PaseoDTO dto, IPaseoService paseoService) =>
            {
                try
                {
                    var found = await paseoService.UpdateAsync(dto);

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
            .WithName("UpdatePaseo")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/paseos/{id}", async (int id, IPaseoService paseoService) =>
            {
                var deleted = await paseoService.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeletePaseo")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
