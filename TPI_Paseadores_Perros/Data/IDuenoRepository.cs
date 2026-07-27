using Domain.Model;

namespace Data
{
    public interface IDuenoRepository
    {
        Task AddAsync(Dueno dueno);
        Task<bool> DeleteAsync(int id);
        Task<Dueno?> GetAsync(int id);
        Task<IEnumerable<Dueno>> GetAllAsync();
        Task<bool> UpdateAsync(Dueno dueno);
        Task<bool> EmailExistsAsync(string email, int? excludeId = null);
    }
}
