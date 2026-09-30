using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    // Los administradores no tienen tabla propia: son filas de Usuarios con Rol = Admin
    // (los dueños y paseadores también están en Usuarios, pero con su tabla hija).
    public class AdministradorRepository : IAdministradorRepository
    {
        private readonly PaseadoresContext _context;
        public AdministradorRepository(PaseadoresContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Usuario administrador)
        {
            _context.Usuarios.Add(administrador);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var administrador = await Administradores().FirstOrDefaultAsync(u => u.Id == id);
            if (administrador != null)
            {
                _context.Usuarios.Remove(administrador);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Usuario?> GetAsync(int id)
        {
            return await Administradores().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await Administradores()
                .OrderBy(u => u.Apellido)
                .ThenBy(u => u.Nombre)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Usuario administrador)
        {
            var existing = await Administradores().FirstOrDefaultAsync(u => u.Id == administrador.Id);
            if (existing != null)
            {
                existing.SetNombre(administrador.Nombre);
                existing.SetApellido(administrador.Apellido);
                existing.SetEmail(administrador.Email);
                existing.SetTelefono(administrador.Telefono);
                existing.SetContrasena(administrador.Contrasena);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // Se busca en todos los usuarios: el login es por email, no puede repetirse
        // aunque el otro sea un dueño o un paseador
        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            var query = _context.Usuarios.Where(u => u.Email.ToLower() == email.ToLower());
            if (excludeId.HasValue)
            {
                query = query.Where(u => u.Id != excludeId.Value);
            }
            return await query.AnyAsync();
        }

        private IQueryable<Usuario> Administradores()
        {
            return _context.Usuarios.Where(u => u.Rol == RolUsuario.Admin);
        }
    }
}
