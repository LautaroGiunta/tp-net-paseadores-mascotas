using DTOs;

namespace Application.Services
{
    public interface ILiquidacionService
    {
        Task<LiquidacionDTO> AddAsync(LiquidacionDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<LiquidacionDTO?> GetAsync(int id);
        Task<IEnumerable<LiquidacionDTO>> GetAllAsync();
        Task<bool> UpdateAsync(LiquidacionDTO dto);
        Task<IEnumerable<PaseoDTO>> GetPaseosPendientesAsync(int paseadorId, DateTime fechaDesde, DateTime fechaHasta);
    }
}
