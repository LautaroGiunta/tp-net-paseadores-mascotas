using System.Security.Claims;

namespace WebAPI
{
    // Datos del usuario logueado, sacados de los claims del token
    public static class UsuarioActual
    {
        // El "sub" del token llega mapeado como NameIdentifier
        public static int ObtenerId(this ClaimsPrincipal usuario)
        {
            return int.Parse(usuario.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        public static bool EsDueno(this ClaimsPrincipal usuario)
        {
            return usuario.IsInRole(Roles.Dueno);
        }
        public static bool EsPaseador(this ClaimsPrincipal usuario)
        {
            return usuario.IsInRole(Roles.Paseador);
        }

        // 403 con el mismo formato { error } que leen los clientes
        public static IResult Prohibido(string mensaje)
        {
            return Results.Json(new { error = mensaje }, statusCode: StatusCodes.Status403Forbidden);
        }
    }
}