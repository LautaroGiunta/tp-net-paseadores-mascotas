using Application.Services;
using DTOs;
using System.Security.Claims;

namespace WebAPI
{
    public static class PaseadorEndpoints
    {
        // El dueño necesita la lista para elegir paseador, pero no los datos de contacto
        private static void OcultarContacto(PaseadorDTO dto)
        {
            dto.Email = "";
            dto.Telefono = "";
        }
        public static void MapPaseadorEndpoints(this WebApplication app)
        {
            app.MapGet("/paseadores/{id}", async (int id, ClaimsPrincipal usuario, IPaseadorService paseadorService) =>
            {
                if (usuario.EsPaseador() && id != usuario.ObtenerId())
                {
                    return UsuarioActual.Prohibido("No podés ver los datos de otro paseador.");
                }

                PaseadorDTO? dto = await paseadorService.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                if (usuario.EsDueno())
                {
                    OcultarContacto(dto);
                }

                return Results.Ok(dto);
            })
            .WithName("GetPaseador")
            .RequireAuthorization()
            .Produces<PaseadorDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/paseadores", async (ClaimsPrincipal usuario, IPaseadorService paseadorService) =>
            {
                // El paseador solo se ve a sí mismo
                if (usuario.EsPaseador())
                {
                    PaseadorDTO? yo = await paseadorService.GetAsync(usuario.ObtenerId());

                    var lista = new List<PaseadorDTO>();
                    if (yo != null)
                    {
                        lista.Add(yo);
                    }
                    return Results.Ok(lista);
                }

                var dtos = (await paseadorService.GetAllAsync()).ToList();

                if (usuario.EsDueno())
                {
                    dtos.ForEach(OcultarContacto);
                }

                return Results.Ok(dtos);
            })
            .WithName("GetAllPaseadores")
            .RequireAuthorization()
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
            .RequireAuthorization(Politicas.SoloAdmin)
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
            .RequireAuthorization(Politicas.SoloAdmin)
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
            .RequireAuthorization(Politicas.SoloAdmin)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/paseadores/{id}", async (int id, IPaseadorService paseadorService) =>
            {
                try
                {
                    var deleted = await paseadorService.DeleteAsync(id);

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
            .WithName("DeletePaseador")
            .RequireAuthorization(Politicas.SoloAdmin)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();
        }
    }
}
