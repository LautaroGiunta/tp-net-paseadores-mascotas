using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DuenoRepository : IDuenoRepository
    {
        private readonly PaseadoresContext _context;
        public DuenoRepository(PaseadoresContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Dueno dueno)
        {
            _context.Duenos.Add(dueno);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var dueno = await _context.Duenos.FindAsync(id);
            if (dueno != null)
            {
                _context.Duenos.Remove(dueno);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Dueno?> GetAsync(int id)
        {
            return await _context.Duenos.FindAsync(id);
        }

        public async Task<IEnumerable<Dueno>> GetAllAsync()
        {
            return await _context.Duenos.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Dueno dueno)
        {
            var existing = await _context.Duenos.FindAsync(dueno.Id);
            if (existing != null)
            {
                existing.SetNombre(dueno.Nombre);
                existing.SetApellido(dueno.Apellido);
                existing.SetEmail(dueno.Email);
                existing.SetTelefono(dueno.Telefono);
                existing.SetDireccion(dueno.Direccion);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            var query = _context.Duenos.AsQueryable(); 
                query = query.Where(d => d.Email.ToLower() == email.ToLower());
            if (excludeId.HasValue)
            {
                query = query.Where(d => d.Id != excludeId.Value);
            }
            return await query.AnyAsync();
        }
    }
}
