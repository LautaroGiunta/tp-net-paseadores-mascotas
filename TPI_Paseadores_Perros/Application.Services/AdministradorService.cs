using Data;
using Domain.Model;
using DTOs;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class AdministradorService : IAdministradorService
    {
        private readonly IAdministradorRepository administradorRepository;

        public AdministradorService(IAdministradorRepository administradorRepository)
        {
            this.administradorRepository = administradorRepository;
        }

        public async Task<UsuarioDTO> AddAsync(UsuarioDTO dto)
        {
            ValidarTelefono(dto.Telefono);

            // Regla de negocio: el email identifica al usuario en el login
            if (await administradorRepository.EmailExistsAsync(dto.Email))
            {
                throw new ArgumentException($"Ya existe un usuario con el email '{dto.Email}'.");
            }

            var fechaAlta = DateTime.Now;
            Usuario administrador = new Usuario(0, dto.Nombre, dto.Apellido, dto.Email,
                                                dto.Telefono, dto.Contrasena, RolUsuario.Admin, fechaAlta);

            await administradorRepository.AddAsync(administrador);

            return MapToDTO(administrador);
        }

        // Regla de negocio: nadie se borra a sí mismo, así siempre queda al menos un administrador
        public async Task<bool> DeleteAsync(int id, int idUsuarioActual)
        {
            if (id == idUsuarioActual)
            {
                throw new ArgumentException("No podés eliminar tu propio usuario.");
            }

            return await administradorRepository.DeleteAsync(id);
        }

        public async Task<UsuarioDTO?> GetAsync(int id)
        {
            Usuario? administrador = await administradorRepository.GetAsync(id);

            if (administrador == null)
                return null;

            return MapToDTO(administrador);
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var administradores = await administradorRepository.GetAllAsync();
            return administradores.Select(MapToDTO).ToList();
        }

        public async Task<bool> UpdateAsync(UsuarioDTO dto)
        {
            ValidarTelefono(dto.Telefono);

            // Regla de negocio: el email no puede estar duplicado (excluyendo el propio usuario)
            if (await administradorRepository.EmailExistsAsync(dto.Email, dto.Id))
            {
                throw new ArgumentException($"Ya existe otro usuario con el email '{dto.Email}'.");
            }

            // Recuperar el existente para preservar la FechaAlta
            var existing = await administradorRepository.GetAsync(dto.Id);
            if (existing == null)
                return false;

            string contrasena = string.IsNullOrWhiteSpace(dto.Contrasena) ? existing.Contrasena : dto.Contrasena;
            Usuario administrador = new Usuario(dto.Id, dto.Nombre, dto.Apellido, dto.Email,
                                                dto.Telefono, contrasena, RolUsuario.Admin, existing.FechaAlta);
            return await administradorRepository.UpdateAsync(administrador);
        }

        // Mismo formato que para dueños y paseadores
        private static void ValidarTelefono(string telefono)
        {
            if (!Regex.IsMatch(telefono ?? "", @"^[0-9]{8,15}$"))
            {
                throw new ArgumentException("El formato del teléfono es inválido. Debe contener entre 8 y 15 números, sin espacios ni letras.");
            }
        }

        private static UsuarioDTO MapToDTO(Usuario administrador)
        {
            return new UsuarioDTO
            {
                Id = administrador.Id,
                Nombre = administrador.Nombre,
                Apellido = administrador.Apellido,
                Email = administrador.Email,
                Telefono = administrador.Telefono,
                // La contraseña no se devuelve
                Rol = administrador.Rol.ToString(),
                FechaAlta = administrador.FechaAlta
            };
        }
    }
}
