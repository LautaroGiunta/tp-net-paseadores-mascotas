using DTOs;

namespace Application.Services
{
    public interface IPerroService
    {
        Task<PerroDTO> AddAsync(PerroDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<PerroDTO?> GetAsync(int id);
        Task<IEnumerable<PerroDTO>> GetAllAsync();
        Task<bool> UpdateAsync(PerroDTO dto);
        Task<IEnumerable<PerroDTO>> GetByDuenoAsync(int duenoId);
    }
}
