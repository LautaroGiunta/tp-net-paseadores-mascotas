using Domain.Model;

namespace Data
{
    public interface IPaseoRepository
    {
        Task AddAsync(Paseo paseo);
        Task<bool> DeleteAsync(int id);
        Task<Paseo?> GetAsync(int id);
        Task<IEnumerable<Paseo>> GetAllAsync();
        Task<bool> UpdateAsync(Paseo paseo);
        Task<IEnumerable<Paseo>> GetByCriteriaAsync(PaseoCriteria criteria);
        Task<bool> ExisteSolapamientoAsync(int paseadorId, DateTime inicio, int duracionMinutos, int? excludeId = null);
        Task<bool> PerroTienePaseosAsync(int perroId);
    }
}
