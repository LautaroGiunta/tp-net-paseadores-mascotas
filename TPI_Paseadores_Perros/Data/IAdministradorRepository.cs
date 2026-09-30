using Domain.Model;

namespace Data
{
    public interface IAdministradorRepository
    {
        Task AddAsync(Usuario administrador);
        Task<bool> DeleteAsync(int id);
        Task<Usuario?> GetAsync(int id);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<bool> UpdateAsync(Usuario administrador);
        Task<bool> EmailExistsAsync(string email, int? excludeId = null);
    }
}
