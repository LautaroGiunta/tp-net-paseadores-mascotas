using Data;
using DTOs;

namespace Application.Services
{
    public class ReporteService : IReporteService
    {
        private readonly IReporteRepository reporteRepository;

        public ReporteService(IReporteRepository reporteRepository)
        {
            this.reporteRepository = reporteRepository;
        }

        public async Task<IEnumerable<RecaudacionMensualDTO>> GetRecaudacionMensualAsync(DateTime fechaDesde, DateTime fechaHasta)
        {
            ValidarPeriodo(fechaDesde, fechaHasta);

            var items = await reporteRepository.GetRecaudacionMensualAsync(fechaDesde, fechaHasta);
            return items.Select(i => new RecaudacionMensualDTO
            {
                Anio = i.Anio,
                Mes = i.Mes,
                PaseadorId = i.PaseadorId,
                PaseadorNombre = $"{i.PaseadorApellido}, {i.PaseadorNombre}",
                CantidadPaseos = i.CantidadPaseos,
                Minutos = i.Minutos,
                Total = i.Total
            }).ToList();
        }

        public async Task<IEnumerable<ActividadPerroDTO>> GetActividadPerrosAsync(DateTime fechaDesde, DateTime fechaHasta)
        {
            ValidarPeriodo(fechaDesde, fechaHasta);

            var items = await reporteRepository.GetActividadPerrosAsync(fechaDesde, fechaHasta);
            return items.Select(i => new ActividadPerroDTO
            {
                PerroId = i.PerroId,
                PerroNombre = i.PerroNombre,
                Raza = i.Raza,
                DuenoNombre = $"{i.DuenoApellido}, {i.DuenoNombre}",
                CantidadPaseos = i.CantidadPaseos,
                Minutos = i.Minutos,
                Total = i.Total,
                UltimoPaseo = i.UltimoPaseo
            }).ToList();
        }

        private static void ValidarPeriodo(DateTime fechaDesde, DateTime fechaHasta)
        {
            if (fechaDesde.Date > fechaHasta.Date)
            {
                throw new ArgumentException("La fecha desde no puede ser posterior a la fecha hasta.");
            }

            // Tope para que un reporte no recorra la base entera por error
            if ((fechaHasta.Date - fechaDesde.Date).TotalDays > 366 * 2)
            {
                throw new ArgumentException("El período del reporte no puede superar los dos años.");
            }
        }
    }
}
