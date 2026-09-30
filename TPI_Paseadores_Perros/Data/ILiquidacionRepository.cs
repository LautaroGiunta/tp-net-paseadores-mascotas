using Domain.Model;

namespace Data
{
    public interface ILiquidacionRepository
    {
        Task AddAsync(Liquidacion liquidacion);
        Task<bool> DeleteAsync(int id);
        Task<Liquidacion?> GetAsync(int id);
        Task<IEnumerable<Liquidacion>> GetAllAsync();
        Task<bool> UpdateAsync(Liquidacion liquidacion);
        Task<IEnumerable<Paseo>> GetPaseosPendientesAsync(int paseadorId, DateTime fechaDesde, DateTime fechaHasta);
        Task<bool> PaseoEstaLiquidadoAsync(int paseoId, int? excludeLiquidacionId = null);
    }
}
