using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class PaseadorRepository : IPaseadorRepository
    {
        private readonly PaseadoresContext _context;
        public PaseadorRepository(PaseadoresContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Paseador paseador)
        {
            _context.Paseadores.Add(paseador);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var paseador = await _context.Paseadores.FindAsync(id);
            if (paseador != null)
            {
                _context.Paseadores.Remove(paseador);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Paseador?> GetAsync(int id)
        {
            return await _context.Paseadores.FindAsync(id);

        }

        public async Task<IEnumerable<Paseador>> GetAllAsync()
        {
            return await _context.Paseadores.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Paseador paseador)
        {
            var existing = await _context.Paseadores.FindAsync(paseador.Id);
            if (existing != null)
            {
                existing.SetNombre(paseador.Nombre);
                existing.SetApellido(paseador.Apellido);
                existing.SetEmail(paseador.Email);
                existing.SetTelefono(paseador.Telefono);
                existing.SetZona(paseador.Zona);
                existing.SetTarifaPorHora(paseador.TarifaPorHora);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            var query = _context.Paseadores.AsQueryable();
            query = query.Where(p => p.Email.ToLower() == email.ToLower());

            if (excludeId.HasValue)
            {
                query = query.Where(p => p.Id != excludeId.Value);
            }
            return await query.AnyAsync();
        }

        public async Task<IEnumerable<Paseador>> GetByCriteriaAsync(PaseadorCriteria criteria)
        {
            string searchTerm = criteria.Texto.ToLower();

            var result =  await _context.Paseadores.
                Where(p =>
                p.Nombre.ToLower().Contains(searchTerm) ||
                p.Apellido.ToLower().Contains(searchTerm) ||
                p.Email.ToLower().Contains(searchTerm) ||
                p.Zona.ToLower().Contains(searchTerm)
            ).OrderBy(p => p.Nombre)
            .ThenBy(p => p.Apellido)
            .ToListAsync();

            return result;
        }
    }
}
