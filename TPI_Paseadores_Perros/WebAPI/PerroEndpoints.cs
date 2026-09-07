using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class PerroEndpoints
    {
        public static void MapPerroEndpoints(this WebApplication app)
        {
            app.MapGet("/perros/{id}", async (int id, IPerroService perroService) =>
            {
                PerroDTO? dto = await perroService.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetPerro")
            .Produces<PerroDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/perros", async (IPerroService perroService) =>
            {
                var dtos = await perroService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllPerros")
            .Produces<List<PerroDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/duenos/{duenoId}/perros", async (int duenoId, IPerroService perroService) =>
            {
                var dtos = await perroService.GetByDuenoAsync(duenoId);
                return Results.Ok(dtos);
            })
            .WithName("GetPerrosByDueno")
            .Produces<List<PerroDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/perros", async (PerroDTO dto, IPerroService perroService) =>
            {
                try
                {
                    PerroDTO perroDTO = await perroService.AddAsync(dto);
                    return Results.Created($"/perros/{perroDTO.Id}", perroDTO);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddPerro")
            .Produces<PerroDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/perros", async (PerroDTO dto, IPerroService perroService) =>
            {
                try
                {
                    var found = await perroService.UpdateAsync(dto);

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
            .WithName("UpdatePerro")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/perros/{id}", async (int id, IPerroService perroService) =>
            {
                try
                {
                    var deleted = await perroService.DeleteAsync(id);

                    if (!deleted)
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
            .WithName("DeletePerro")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();
        }
    }
}
