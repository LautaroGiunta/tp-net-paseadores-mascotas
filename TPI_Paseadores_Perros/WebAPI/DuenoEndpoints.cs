using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class DuenoEndpoints
    {
        public static void MapDuenoEndpoints(this WebApplication app)
        {
            app.MapGet("/duenos/{id}", async (int id, IDuenoService duenoService) =>
            {
                DuenoDTO? dto = await duenoService.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetDueno")
            .Produces<DuenoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/duenos", async (IDuenoService duenoService) =>
            {
                var dtos = await duenoService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllDuenos")
            .Produces<List<DuenoDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/duenos", async (DuenoDTO dto, IDuenoService duenoService) =>
            {
                try
                {
                    DuenoDTO duenoDTO = await duenoService.AddAsync(dto);
                    return Results.Created($"/duenos/{duenoDTO.Id}", duenoDTO);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddDueno")
            .Produces<DuenoDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/duenos", async (DuenoDTO dto, IDuenoService duenoService) =>
            {
                try
                {
                    var found = await duenoService.UpdateAsync(dto);

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
            .WithName("UpdateDueno")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/duenos/{id}", async (int id, IDuenoService duenoService) =>
            {
                var deleted = await duenoService.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeleteDueno")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
