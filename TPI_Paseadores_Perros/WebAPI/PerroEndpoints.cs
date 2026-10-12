using Application.Services;
using DTOs;
using System.Security.Claims;

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
            .RequireAuthorization()
            .Produces<PerroDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/perros", async (IPerroService perroService) =>
            {
                var dtos = await perroService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllPerros")
            .RequireAuthorization()
            .Produces<List<PerroDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/duenos/{duenoId}/perros", async (int duenoId, IPerroService perroService) =>
            {
                var dtos = await perroService.GetByDuenoAsync(duenoId);
                return Results.Ok(dtos);
            })
            .WithName("GetPerrosByDueno")
            .RequireAuthorization()
            .Produces<List<PerroDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/perros", async (PerroDTO dto, ClaimsPrincipal usuario, IPerroService perroService) =>
            {
                if(usuario.EsDueno() && dto.DuenoId != usuario.ObtenerId())
                {
                    return UsuarioActual.Prohibido("No podés cargar perros a nombre de otro dueño.");
                }
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
            .RequireAuthorization(Politicas.AdminODueno)
            .Produces<PerroDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/perros", async (PerroDTO dto, ClaimsPrincipal usuario, IPerroService perroService) =>
            {
                if (usuario.EsDueno())
                {
                    PerroDTO? actual = await perroService.GetAsync(dto.Id);

                    if (actual != null && actual.DuenoId != usuario.ObtenerId())
                    {
                        return UsuarioActual.Prohibido("No podés modificar un perro de otro dueño.");
                    }

                    if (dto.DuenoId != usuario.ObtenerId())
                    {
                        return UsuarioActual.Prohibido("No podés pasarle el perro a otro dueño.");
                    }
                }
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
            .RequireAuthorization(Politicas.AdminODueno)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/perros/{id}", async (int id, ClaimsPrincipal usuario, IPerroService perroService) =>
            {
                if (usuario.EsDueno())
                {
                    PerroDTO? actual = await perroService.GetAsync(id);

                    if (actual != null && actual.DuenoId != usuario.ObtenerId())
                    {
                        return UsuarioActual.Prohibido("No podés eliminar un perro de otro dueño.");
                    }
                }
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
            .RequireAuthorization(Politicas.AdminODueno)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();
        }
    }
}
