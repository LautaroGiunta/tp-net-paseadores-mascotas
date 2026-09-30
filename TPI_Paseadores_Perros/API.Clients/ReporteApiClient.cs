using DTOs;

namespace API.Clients
{
    public class ReporteApiClient : BaseApiClient
    {
        public ReporteApiClient(HttpClient http) : base(http) { }

        public async Task<List<RecaudacionMensualDTO>> GetRecaudacionMensualAsync(DateTime fechaDesde, DateTime fechaHasta)
            => await GetListAsync<RecaudacionMensualDTO>(
                $"/reportes/recaudacion-mensual?fechaDesde={fechaDesde:yyyy-MM-dd}&fechaHasta={fechaHasta:yyyy-MM-dd}");

        public async Task<List<ActividadPerroDTO>> GetActividadPerrosAsync(DateTime fechaDesde, DateTime fechaHasta)
            => await GetListAsync<ActividadPerroDTO>(
                $"/reportes/actividad-perros?fechaDesde={fechaDesde:yyyy-MM-dd}&fechaHasta={fechaHasta:yyyy-MM-dd}");
    }
}
