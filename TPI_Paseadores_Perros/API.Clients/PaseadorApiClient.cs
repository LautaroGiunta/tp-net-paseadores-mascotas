using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Clients
{
    public class PaseadorApiClient : BaseApiClient
    {
        public PaseadorApiClient(HttpClient http) : base(http) { }

        public async Task<List<PaseadorDTO>> GetAllAsync()
            => await GetListAsync<PaseadorDTO>("/paseadores");

        public async Task<PaseadorDTO?> GetAsync(int id)
            => await GetAsync<PaseadorDTO>($"/paseadores/{id}");

        // La API espera /paseadores/buscar?texto=...
        public async Task<List<PaseadorDTO>> BuscarAsync(string texto)
            => await GetListAsync<PaseadorDTO>($"/paseadores/buscar?texto={Uri.EscapeDataString(texto ?? "")}");

        public async Task<PaseadorDTO?> AddAsync(PaseadorDTO paseador)
            => await PostAsync("/paseadores", paseador);

        public async Task UpdateAsync(PaseadorDTO paseador)
            => await PutAsync("/paseadores", paseador);

        public async Task DeleteAsync(int id)
            => await DeleteAsync($"/paseadores/{id}");
    }
}
