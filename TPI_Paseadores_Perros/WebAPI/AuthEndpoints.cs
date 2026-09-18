using Application.Services;
using DTOs;

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
                    return Results.BadRequest(new { error = "El email y la contraseña son obligatorios." });
                }

                var respuesta = await authService.LoginAsync(dto);

                if (respuesta == null)
                {
                    return Results.Json(new { mensaje = "Email o contraseña incorrectos." }, statusCode: StatusCodes.Status401Unauthorized);
                }

                return Results.Ok(respuesta);
            })
            .WithName("Login")
            // El único endpoint que no pide token
            .AllowAnonymous()
            .Produces<LoginResponseDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithOpenApi();
        }
    }
}
