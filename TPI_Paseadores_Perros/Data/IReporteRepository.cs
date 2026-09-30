namespace Data
{
    public interface IReporteRepository
    {
        Task<IEnumerable<RecaudacionMensualItem>> GetRecaudacionMensualAsync(DateTime fechaDesde, DateTime fechaHasta);
        Task<IEnumerable<ActividadPerroItem>> GetActividadPerrosAsync(DateTime fechaDesde, DateTime fechaHasta);
    }
}
