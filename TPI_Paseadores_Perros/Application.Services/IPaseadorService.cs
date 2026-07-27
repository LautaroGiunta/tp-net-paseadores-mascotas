using DTOs;

namespace Application.Services
{
    public interface IPaseadorService
    {
        Task<PaseadorDTO> AddAsync(PaseadorDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<PaseadorDTO?> GetAsync(int id);
        Task<IEnumerable<PaseadorDTO>> GetAllAsync();
        Task<bool> UpdateAsync(PaseadorDTO dto);
        Task<IEnumerable<PaseadorDTO>> GetByCriteriaAsync(PaseadorCriteriaDTO criteriaDTO);
    }
}
