using Domain.Model;

namespace Data
{
    public class PaseadorRepository : IPaseadorRepository
    {
        private static readonly List<Paseador> paseadores = new List<Paseador>();
        private static int nextId = 1;

        public Task AddAsync(Paseador paseador)
        {
            paseador.SetId(nextId);
            nextId++;

            paseadores.Add(paseador);
            return Task.CompletedTask;
        }

        public Task<bool> DeleteAsync(int id)
        {
            var paseador = paseadores.FirstOrDefault(p => p.Id == id);
            if (paseador != null)
            {
                paseadores.Remove(paseador);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<Paseador?> GetAsync(int id)
        {
            return Task.FromResult(paseadores.FirstOrDefault(p => p.Id == id));
        }

        public Task<IEnumerable<Paseador>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Paseador>>(paseadores.ToList());
        }

        public Task<bool> UpdateAsync(Paseador paseador)
        {
            var existing = paseadores.FirstOrDefault(p => p.Id == paseador.Id);
            if (existing != null)
            {
                existing.SetNombre(paseador.Nombre);
                existing.SetApellido(paseador.Apellido);
                existing.SetEmail(paseador.Email);
                existing.SetTelefono(paseador.Telefono);
                existing.SetZona(paseador.Zona);
                existing.SetTarifaPorHora(paseador.TarifaPorHora);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            var query = paseadores.Where(p => p.Email.ToLower() == email.ToLower());
            if (excludeId.HasValue)
            {
                query = query.Where(p => p.Id != excludeId.Value);
            }
            return Task.FromResult(query.Any());
        }

        public Task<IEnumerable<Paseador>> GetByCriteriaAsync(PaseadorCriteria criteria)
        {
            string searchTerm = criteria.Texto.ToLower();

            IEnumerable<Paseador> result = paseadores.Where(p =>
                p.Nombre.ToLower().Contains(searchTerm) ||
                p.Apellido.ToLower().Contains(searchTerm) ||
                p.Email.ToLower().Contains(searchTerm) ||
                p.Zona.ToLower().Contains(searchTerm)
            ).OrderBy(p => p.Nombre).ThenBy(p => p.Apellido).ToList();

            return Task.FromResult(result);
        }
    }
}
