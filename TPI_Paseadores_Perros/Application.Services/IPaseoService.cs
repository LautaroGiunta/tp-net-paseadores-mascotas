using DTOs;

namespace Application.Services
{
    public interface IPaseoService
    {
        Task<PaseoDTO> AddAsync(PaseoDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<PaseoDTO?> GetAsync(int id);
        Task<IEnumerable<PaseoDTO>> GetAllAsync();
        Task<bool> UpdateAsync(PaseoDTO dto);
        Task<IEnumerable<PaseoDTO>> GetByCriteriaAsync(PaseoCriteriaDTO criteriaDTO);
    }
}
