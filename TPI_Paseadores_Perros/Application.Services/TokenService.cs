using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DTOs;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtConfig _config;

        public TokenService(JwtConfig config)
        {
            _config = config;
        }

        public string GenerarToken(UsuarioDTO usuario)
        {
            var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.ClaveSecreta));
            var credenciales = new SigningCredentials(clave, SecurityAlgorithms.HmacSha256);

            // El rol va como claim adentro del token: es lo que después lee RequireRole en la API
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var token = new JwtSecurityToken(
                issuer: _config.Emisor,
                audience: _config.Audiencia,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_config.MinutosDeExpiracion),
                signingCredentials: credenciales);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
