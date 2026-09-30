using DTOs;

namespace API.Clients
{
    public class LiquidacionApiClient : BaseApiClient
    {
        public LiquidacionApiClient(HttpClient http) : base(http) { }

        public async Task<List<LiquidacionDTO>> GetAllAsync()
            => await GetListAsync<LiquidacionDTO>("/liquidaciones");

        public async Task<LiquidacionDTO?> GetAsync(int id)
            => await GetAsync<LiquidacionDTO>($"/liquidaciones/{id}");

        public async Task<List<PaseoDTO>> GetPaseosPendientesAsync(int paseadorId, DateTime fechaDesde, DateTime fechaHasta)
            => await GetListAsync<PaseoDTO>($"/liquidaciones/paseos-pendientes?paseadorId={paseadorId}" +
                                            $"&fechaDesde={fechaDesde:yyyy-MM-dd}&fechaHasta={fechaHasta:yyyy-MM-dd}");

        public async Task<LiquidacionDTO?> AddAsync(LiquidacionDTO liquidacion)
            => await PostAsync("/liquidaciones", liquidacion);

        public async Task UpdateAsync(LiquidacionDTO liquidacion)
            => await PutAsync("/liquidaciones", liquidacion);

        public async Task DeleteAsync(int id)
            => await DeleteAsync($"/liquidaciones/{id}");
    }
}
