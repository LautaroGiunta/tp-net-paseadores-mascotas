namespace Domain.Model
{
    public class PaseoCriteria
    {
        public int? PaseadorId { get; private set; }
        public int? PerroId { get; private set; }
        public DateTime? FechaDesde { get; private set; }
        public DateTime? FechaHasta { get; private set; }

        public PaseoCriteria(int? paseadorId, int? perroId, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            if (fechaDesde.HasValue && fechaHasta.HasValue && fechaDesde > fechaHasta)
                throw new ArgumentException("La fecha desde no puede ser posterior a la fecha hasta.", nameof(fechaDesde));

            PaseadorId = paseadorId;
            PerroId = perroId;
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
        }
    }
}
