using Domain.Model;

namespace Data
{
    public class DuenoRepository : IDuenoRepository
    {
        private static readonly List<Dueno> duenos = new List<Dueno>();
        private static int nextId = 1;

        public Task AddAsync(Dueno dueno)
        {
            dueno.SetId(nextId);
            nextId++;

            duenos.Add(dueno);
            return Task.CompletedTask;
        }

        public Task<bool> DeleteAsync(int id)
        {
            var dueno = duenos.FirstOrDefault(d => d.Id == id);
            if (dueno != null)
            {
                duenos.Remove(dueno);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<Dueno?> GetAsync(int id)
        {
            return Task.FromResult(duenos.FirstOrDefault(d => d.Id == id));
        }

        public Task<IEnumerable<Dueno>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Dueno>>(duenos.ToList());
        }

        public Task<bool> UpdateAsync(Dueno dueno)
        {
            var existing = duenos.FirstOrDefault(d => d.Id == dueno.Id);
            if (existing != null)
            {
                existing.SetNombre(dueno.Nombre);
                existing.SetApellido(dueno.Apellido);
                existing.SetEmail(dueno.Email);
                existing.SetTelefono(dueno.Telefono);
                existing.SetDireccion(dueno.Direccion);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            var query = duenos.Where(d => d.Email.ToLower() == email.ToLower());
            if (excludeId.HasValue)
            {
                query = query.Where(d => d.Id != excludeId.Value);
            }
            return Task.FromResult(query.Any());
        }
    }
}
