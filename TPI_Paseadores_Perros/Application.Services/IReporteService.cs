using DTOs;

namespace Application.Services
{
    public interface IReporteService
    {
        Task<IEnumerable<RecaudacionMensualDTO>> GetRecaudacionMensualAsync(DateTime fechaDesde, DateTime fechaHasta);
        Task<IEnumerable<ActividadPerroDTO>> GetActividadPerrosAsync(DateTime fechaDesde, DateTime fechaHasta);
    }
}
