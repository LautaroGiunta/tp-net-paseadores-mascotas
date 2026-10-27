using Domain.Model;

namespace Data
{
    public interface IPaseadorRepository
    {
        Task AddAsync(Paseador paseador);
        Task<bool> DeleteAsync(int id);
        Task<Paseador?> GetAsync(int id);
        Task<IEnumerable<Paseador>> GetAllAsync();
        Task<bool> UpdateAsync(Paseador paseador);
        Task<IEnumerable<Paseador>> GetByCriteriaAsync(PaseadorCriteria criteria);
    }
}
