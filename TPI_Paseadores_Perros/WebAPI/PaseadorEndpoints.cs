using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class PaseadorEndpoints
    {
        public static void MapPaseadorEndpoints(this WebApplication app)
        {
            app.MapGet("/paseadores/{id}", async (int id, IPaseadorService paseadorService) =>
            {
                PaseadorDTO? dto = await paseadorService.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetPaseador")
            .Produces<PaseadorDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/paseadores", async (IPaseadorService paseadorService) =>
            {
                var dtos = await paseadorService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllPaseadores")
            .Produces<List<PaseadorDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/paseadores/buscar", async (string texto, IPaseadorService paseadorService) =>
            {
                try
                {
                    var criteria = new PaseadorCriteriaDTO { Texto = texto };
                    var paseadores = await paseadorService.GetByCriteriaAsync(criteria);
                    return Results.Ok(paseadores);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("GetPaseadoresByCriteria")
            .Produces<List<PaseadorDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPost("/paseadores", async (PaseadorDTO dto, IPaseadorService paseadorService) =>
            {
                try
                {
                    PaseadorDTO paseadorDTO = await paseadorService.AddAsync(dto);
                    return Results.Created($"/paseadores/{paseadorDTO.Id}", paseadorDTO);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddPaseador")
            .Produces<PaseadorDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/paseadores", async (PaseadorDTO dto, IPaseadorService paseadorService) =>
            {
                try
                {
                    var found = await paseadorService.UpdateAsync(dto);

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
            .WithName("UpdatePaseador")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/paseadores/{id}", async (int id, IPaseadorService paseadorService) =>
            {
                var deleted = await paseadorService.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeletePaseador")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
