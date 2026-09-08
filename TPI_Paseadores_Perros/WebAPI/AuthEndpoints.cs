using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
namespace WebAPI
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/auth/login", async (LoginRequestDTO dto, IAuthService authService) =>
            {
                if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Contrasena))
                {
                    return Results.BadRequest("El email y la contraseña son obligatorios.");
                }

                var usuarioLogueado = await authService.LoginAsync(dto);

                if (usuarioLogueado == null)
                {
                    return Results.Json(new { mensaje = "Email o contraseña incorrectos." }, statusCode: StatusCodes.Status401Unauthorized);
                }

                return Results.Ok(usuarioLogueado);
            });
        }
    }
}
