using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class LiquidacionRepository : ILiquidacionRepository
    {
        private readonly PaseadoresContext _context;
        public LiquidacionRepository(PaseadoresContext context)
        {
            _context = context;
        }

        // Cabecera y líneas se insertan juntas en un solo SaveChanges (una transacción)
        public async Task AddAsync(Liquidacion liquidacion)
        {
            _context.Liquidaciones.Add(liquidacion);
            await _context.SaveChangesAsync();
        }

        // Las líneas se borran solas por el Cascade de la relación
        public async Task<bool> DeleteAsync(int id)
        {
            var liquidacion = await _context.Liquidaciones.FindAsync(id);
            if (liquidacion != null)
            {
                _context.Liquidaciones.Remove(liquidacion);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Liquidacion?> GetAsync(int id)
        {
            return await ConDetalles().FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Liquidacion>> GetAllAsync()
        {
            return await ConDetalles()
                .OrderByDescending(l => l.FechaDesde)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Liquidacion liquidacion)
        {
            var existing = await ConDetalles().FirstOrDefaultAsync(l => l.Id == liquidacion.Id);
            if (existing != null)
            {
                // Las líneas que salen de la colección EF las borra al guardar
                existing.ActualizarDesde(liquidacion);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // Paseos del paseador en el período que ya terminaron y no están en ninguna liquidación
        public async Task<IEnumerable<Paseo>> GetPaseosPendientesAsync(int paseadorId, DateTime fechaDesde, DateTime fechaHasta)
        {
            DateTime desde = fechaDesde.Date;
            DateTime hastaExclusivo = fechaHasta.Date.AddDays(1);
            DateTime ahora = DateTime.Now;

            return await _context.Paseos
                .Where(p => p.PaseadorId == paseadorId &&
                            p.FechaHoraInicio >= desde &&
                            p.FechaHoraInicio < hastaExclusivo &&
                            p.FechaHoraInicio.AddMinutes(p.DuracionMinutos) <= ahora &&
                            !_context.LiquidacionDetalles.Any(d => d.PaseoId == p.Id))
                .OrderBy(p => p.FechaHoraInicio)
                .Include(p => p.Paseador)
                .Include(p => p.Perro)
                .ToListAsync();
        }

        public async Task<bool> PaseoEstaLiquidadoAsync(int paseoId, int? excludeLiquidacionId = null)
        {
            var query = _context.LiquidacionDetalles.Where(d => d.PaseoId == paseoId);

            if (excludeLiquidacionId.HasValue)
                query = query.Where(d => d.LiquidacionId != excludeLiquidacionId.Value);

            return await query.AnyAsync();
        }

        private IQueryable<Liquidacion> ConDetalles()
        {
            return _context.Liquidaciones
                .Include(l => l.Paseador)
                .Include(l => l.Detalles)
                    .ThenInclude(d => d.Paseo)
                        .ThenInclude(p => p!.Perro);
        }
    }
}
