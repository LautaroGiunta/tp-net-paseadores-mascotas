using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Clients
{
    public class PaseoApiClient : BaseApiClient
    {
        public PaseoApiClient(HttpClient http) : base(http) { }

        public async Task<List<PaseoDTO>> GetAllAsync()
            => await GetListAsync<PaseoDTO>("/paseos");

        public async Task<PaseoDTO?> GetAsync(int id)
            => await GetAsync<PaseoDTO>($"/paseos/{id}");

        // Arma el query string solo con los filtros que llegaron con valor
        public async Task<List<PaseoDTO>> BuscarAsync(int? paseadorId, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            var filtros = new List<string>();
            if (paseadorId.HasValue) filtros.Add($"paseadorId={paseadorId.Value}");
            if (fechaDesde.HasValue) filtros.Add($"fechaDesde={fechaDesde.Value:yyyy-MM-ddTHH:mm:ss}");
            if (fechaHasta.HasValue) filtros.Add($"fechaHasta={fechaHasta.Value:yyyy-MM-ddTHH:mm:ss}");

            string url = "/paseos/buscar";
            if (filtros.Count > 0) url += "?" + string.Join("&", filtros);

            return await GetListAsync<PaseoDTO>(url);
        }

        public async Task<PaseoDTO?> AddAsync(PaseoDTO paseo)
            => await PostAsync("/paseos", paseo);

        public async Task UpdateAsync(PaseoDTO paseo)
            => await PutAsync("/paseos", paseo);

        public async Task DeleteAsync(int id)
            => await DeleteAsync($"/paseos/{id}");
    }
}
