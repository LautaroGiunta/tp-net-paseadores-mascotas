using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Services;
using DTOs;

namespace WebAPI
{
    // Los usuarios administradores los gestiona solo otro Admin
    public static class AdministradorEndpoints
    {
        public static void MapAdministradorEndpoints(this WebApplication app)
        {
            app.MapGet("/administradores/{id}", async (int id, IAdministradorService administradorService) =>
            {
                UsuarioDTO? dto = await administradorService.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetAdministrador")
            .RequireAuthorization(Politicas.SoloAdmin)
            .Produces<UsuarioDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/administradores", async (IAdministradorService administradorService) =>
            {
                var dtos = await administradorService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllAdministradores")
            .RequireAuthorization(Politicas.SoloAdmin)
            .Produces<List<UsuarioDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/administradores", async (UsuarioDTO dto, IAdministradorService administradorService) =>
            {
                try
                {
                    UsuarioDTO administradorDTO = await administradorService.AddAsync(dto);
                    return Results.Created($"/administradores/{administradorDTO.Id}", administradorDTO);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddAdministrador")
            .RequireAuthorization(Politicas.SoloAdmin)
            .Produces<UsuarioDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/administradores", async (UsuarioDTO dto, IAdministradorService administradorService) =>
            {
                try
                {
                    var found = await administradorService.UpdateAsync(dto);

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
            .WithName("UpdateAdministrador")
            .RequireAuthorization(Politicas.SoloAdmin)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            // El que borra es el usuario del token: así el servicio sabe si se está borrando a sí mismo
            app.MapDelete("/administradores/{id}", async (int id, ClaimsPrincipal usuario, IAdministradorService administradorService) =>
            {
                try
                {
                    var deleted = await administradorService.DeleteAsync(id, IdDelUsuario(usuario));

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
            .WithName("DeleteAdministrador")
            .RequireAuthorization(Politicas.SoloAdmin)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();
        }

        // El Id viaja en el claim "sub" del token; al validarlo, ASP.NET lo renombra a NameIdentifier
        private static int IdDelUsuario(ClaimsPrincipal usuario)
        {
            string? valor = usuario.FindFirstValue(ClaimTypes.NameIdentifier)
                            ?? usuario.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return int.TryParse(valor, out int id) ? id : 0;
        }
    }
}
