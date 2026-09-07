using Domain.Model;

namespace Data
{
    public interface IPerroRepository
    {
        Task AddAsync(Perro perro);
        Task<bool> DeleteAsync(int id);
        Task<Perro?> GetAsync(int id);
        Task<IEnumerable<Perro>> GetAllAsync();
        Task<bool> UpdateAsync(Perro perro);
        Task<IEnumerable<Perro>> GetByDuenoAsync(int duenoId);
    }
}
