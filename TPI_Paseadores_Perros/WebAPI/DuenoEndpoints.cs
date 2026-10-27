using Application.Services;
using DTOs;
using System.Security.Claims;

namespace WebAPI
{
    public static class DuenoEndpoints
    {
        public static void MapDuenoEndpoints(this WebApplication app)
        {
            app.MapGet("/duenos/{id}", async (int id, ClaimsPrincipal usuario, IDuenoService duenoService) =>
            {
                if (usuario.EsDueno() && id != usuario.ObtenerId())
                {
                    return UsuarioActual.Prohibido("No podés ver los datos de otro dueño.");
                }

                DuenoDTO? dto = await duenoService.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetDueno")
            .RequireAuthorization(Politicas.AdminODueno)
            .Produces<DuenoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/duenos", async (ClaimsPrincipal usuario, IDuenoService duenoService) =>
            {
                // El dueño solo se ve a sí mismo
                if (usuario.EsDueno())
                {
                    DuenoDTO? yo = await duenoService.GetAsync(usuario.ObtenerId());
                    var lista = new List<DuenoDTO>();
                    if (yo != null)
                    {
                        lista.Add(yo);
                    }
                    return Results.Ok(lista);
                }

                var dtos = await duenoService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllDuenos")
            .RequireAuthorization(Politicas.AdminODueno)
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
            .RequireAuthorization(Politicas.SoloAdmin)
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
            .RequireAuthorization(Politicas.SoloAdmin)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/duenos/{id}", async (int id, IDuenoService duenoService) =>
            {
                try
                {
                    var deleted = await duenoService.DeleteAsync(id);

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
            .WithName("DeleteDueno")
            .RequireAuthorization(Politicas.SoloAdmin)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();
        }
    }
}
