using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class PerroRepository : IPerroRepository
    {
        private readonly PaseadoresContext _context;
        public PerroRepository(PaseadoresContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Perro perro)
        {
            _context.Perros.Add(perro);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var perro = await _context.Perros.FindAsync(id);
            if (perro != null)
            {
                _context.Perros.Remove(perro);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Perro?> GetAsync(int id)
        {
            return await _context.Perros.FindAsync(id);
        }

        public async Task<IEnumerable<Perro>> GetAllAsync()
        {
            return await _context.Perros.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Perro perro)
        {
            var existing = await _context.Perros.FindAsync(perro.Id);
            if (existing != null)
            {
                existing.SetDuenoId(perro.DuenoId);
                existing.SetNombre(perro.Nombre);
                existing.SetRaza(perro.Raza);
                existing.SetEdad(perro.Edad);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Perro>> GetByDuenoAsync(int duenoId)
        {
            return await _context.Perros
                .Where(p => p.DuenoId == duenoId)
                .OrderBy(p => p.Nombre)
                .ToListAsync();
        }
    }
}
