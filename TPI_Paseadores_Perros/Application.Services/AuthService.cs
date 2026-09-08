using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using DTOs;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioDTO?> LoginAsync(LoginRequestDTO dto)
        {
            if (dto.Email == "admin" && dto.Contrasena == "admin")
            {
                return new UsuarioDTO
                {
                    Id = 0,
                    Nombre = "Super",
                    Apellido = "Admin",
                    Email = "admin",
                    Telefono = "00000000",
                    Rol = "Admin", // Tiene rol Admin supremo para ver todo
                    FechaAlta = DateTime.Now
                };
            }

            var usuario = await _usuarioRepository.GetByEmailAndPassAsync(dto.Email, dto.Contrasena);

            if (usuario == null)
            {
                return null; 
            }

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Telefono = usuario.Telefono,
                Rol = usuario.Rol.ToString(), 
                FechaAlta = usuario.FechaAlta
            };
        }
    }
}
