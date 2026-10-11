using Data;
using DTOs;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO dto)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(dto.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Contrasena, usuario.Contrasena))
            {
                return null;
            }

            // Sin la contraseña, este DTO viaja al cliente
            var usuarioDTO = new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Telefono = usuario.Telefono,
                Rol = usuario.Rol.ToString(),
                FechaAlta = usuario.FechaAlta
            };

            return new LoginResponseDTO
            {
                Token = _tokenService.GenerarToken(usuarioDTO),
                Usuario = usuarioDTO
            };
        }
    }
}
