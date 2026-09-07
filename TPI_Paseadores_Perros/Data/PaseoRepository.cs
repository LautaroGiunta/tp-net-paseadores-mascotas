using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class PaseoRepository : IPaseoRepository
    {
        private readonly PaseadoresContext _context;
        public PaseoRepository(PaseadoresContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Paseo paseo)
        {
            _context.Paseos.Add(paseo);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var paseo = await _context.Paseos.FindAsync(id);
            if (paseo != null)
            {
                _context.Paseos.Remove(paseo);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Paseo?> GetAsync(int id)
        {
            return await _context.Paseos.FindAsync(id);
        }

        public async Task<IEnumerable<Paseo>> GetAllAsync()
        {
            return await _context.Paseos
                .OrderBy(p => p.FechaHoraInicio)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Paseo paseo)
        {
            var existing = await _context.Paseos.FindAsync(paseo.Id);
            if (existing != null)
            {
                existing.SetPaseadorId(paseo.PaseadorId);
                existing.SetPerroId(paseo.PerroId);
                existing.SetFechaHoraInicio(paseo.FechaHoraInicio);
                existing.SetDuracionMinutos(paseo.DuracionMinutos);
                existing.SetPrecioTotal(paseo.PrecioTotal);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Paseo>> GetByCriteriaAsync(PaseoCriteria criteria)
        {
            var query = _context.Paseos.AsQueryable();

            if (criteria.PaseadorId.HasValue)
                query = query.Where(p => p.PaseadorId == criteria.PaseadorId.Value);

            if (criteria.PerroId.HasValue)
                query = query.Where(p => p.PerroId == criteria.PerroId.Value);

            if (criteria.FechaDesde.HasValue)
                query = query.Where(p => p.FechaHoraInicio >= criteria.FechaDesde.Value);

            if (criteria.FechaHasta.HasValue)
                query = query.Where(p => p.FechaHoraInicio <= criteria.FechaHasta.Value);

            return await query
                .OrderBy(p => p.FechaHoraInicio)
                .ToListAsync();
        }

        // Dos paseos se pisan si uno empieza antes de que el otro termine, y viceversa.
        public async Task<bool> ExisteSolapamientoAsync(int paseadorId, DateTime inicio, int duracionMinutos, int? excludeId = null)
        {
            DateTime fin = inicio.AddMinutes(duracionMinutos);

            var query = _context.Paseos.Where(p => p.PaseadorId == paseadorId);

            if (excludeId.HasValue)
                query = query.Where(p => p.Id != excludeId.Value);

            return await query.AnyAsync(p =>
                p.FechaHoraInicio < fin &&
                inicio < p.FechaHoraInicio.AddMinutes(p.DuracionMinutos));
        }

        public async Task<bool> PerroTienePaseosAsync(int perroId)
        {
            return await _context.Paseos.AnyAsync(p => p.PerroId == perroId);
        }
    }
}
