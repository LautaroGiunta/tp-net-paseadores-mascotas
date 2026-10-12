using Application.Services;
using DTOs;
using System.Security.Claims;

namespace WebAPI
{
    public static class PaseoEndpoints
    {
        // El dueño de un paseo es el dueño del perro.
        // Si el perro no existe devolvemos true y dejamos que el service tire su error.
        private static async Task<bool> EsPerroDelUsuario(int perroId, ClaimsPrincipal usuario, IPerroService perroService)
        {
            PerroDTO? perro = await perroService.GetAsync(perroId);
            return perro == null || perro.DuenoId == usuario.ObtenerId();
        }

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
            .RequireAuthorization()
            .Produces<PaseoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/paseos", async (IPaseoService paseoService) =>
            {
                var dtos = await paseoService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllPaseos")
            .RequireAuthorization()
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
            .RequireAuthorization()
            .Produces<List<PaseoDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPost("/paseos", async (PaseoDTO dto, ClaimsPrincipal usuario, IPaseoService paseoService, IPerroService perroService) =>
            {
                if (usuario.EsDueno() && !await EsPerroDelUsuario(dto.PerroId, usuario, perroService))
                {
                    return UsuarioActual.Prohibido("No podés pedir un paseo para un perro de otro dueño.");
                }
                if (usuario.EsPaseador() && dto.PaseadorId != usuario.ObtenerId())
                {
                    return UsuarioActual.Prohibido("No podés registrar un paseo a nombre de otro paseador.");
                }
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
            .RequireAuthorization(Politicas.GestionDePaseos)
            .Produces<PaseoDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/paseos", async (PaseoDTO dto, ClaimsPrincipal usuario,
                             IPaseoService paseoService, IPerroService perroService) =>
            {
                if (usuario.EsDueno())
                {
                    PaseoDTO? actual = await paseoService.GetAsync(dto.Id);

                    if (actual != null && !await EsPerroDelUsuario(actual.PerroId, usuario, perroService))
                    {
                        return UsuarioActual.Prohibido("No podés modificar un paseo de otro dueño.");
                    }

                    if (!await EsPerroDelUsuario(dto.PerroId, usuario, perroService))
                    {
                        return UsuarioActual.Prohibido("No podés asignarle el paseo a un perro de otro dueño.");
                    }
                }
                if (usuario.EsPaseador())
                {
                    PaseoDTO? actual = await paseoService.GetAsync(dto.Id);

                    if (actual != null && actual.PaseadorId != usuario.ObtenerId())
                    {
                        return UsuarioActual.Prohibido("No podés modificar un paseo de otro paseador.");
                    }

                    if (dto.PaseadorId != usuario.ObtenerId())
                    {
                        return UsuarioActual.Prohibido("No podés pasarle el paseo a otro paseador.");
                    }
                }
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
            .RequireAuthorization(Politicas.GestionDePaseos)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/paseos/{id}", async (int id, ClaimsPrincipal usuario,
                                    IPaseoService paseoService, IPerroService perroService) =>
            {
                if (usuario.EsDueno())
                {
                    PaseoDTO? actual = await paseoService.GetAsync(id);

                    if (actual != null && !await EsPerroDelUsuario(actual.PerroId, usuario, perroService))
                    {
                        return UsuarioActual.Prohibido("No podés eliminar un paseo de otro dueño.");
                    }
                }
                if (usuario.EsPaseador())
                {
                    PaseoDTO? actual = await paseoService.GetAsync(id);

                    if (actual != null && actual.PaseadorId != usuario.ObtenerId())
                    {
                        return UsuarioActual.Prohibido("No podés eliminar un paseo de otro paseador.");
                    }
                }
                try
                {
                    var deleted = await paseoService.DeleteAsync(id);

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
            .WithName("DeletePaseo")
            .RequireAuthorization(Politicas.GestionDePaseos)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();
        }
    }
}
