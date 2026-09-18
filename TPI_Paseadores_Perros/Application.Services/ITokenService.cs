using DTOs;

namespace Application.Services
{
    public interface ITokenService
    {
        string GenerarToken(UsuarioDTO usuario);
    }
}
