using DTOs;

namespace Application.Services
{
    public interface IDuenoService
    {
        Task<DuenoDTO> AddAsync(DuenoDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<DuenoDTO?> GetAsync(int id);
        Task<IEnumerable<DuenoDTO>> GetAllAsync();
        Task<bool> UpdateAsync(DuenoDTO dto);
    }
}
