namespace Domain.Model
{
    // Maestro del patrón Maestro/Detalle: lo que se le paga a un paseador por
    // los paseos que hizo en un período. Las líneas solo se tocan desde acá.
    public class Liquidacion
    {
        public int Id { get; private set; }
        public int PaseadorId { get; private set; }
        public DateTime FechaDesde { get; private set; }
        public DateTime FechaHasta { get; private set; }
        public DateTime FechaAlta { get; private set; }
        public Paseador? Paseador { get; private set; }

        private readonly List<LiquidacionDetalle> _detalles = new List<LiquidacionDetalle>();
        public IReadOnlyCollection<LiquidacionDetalle> Detalles => _detalles;

        public Liquidacion(int id, int paseadorId, DateTime fechaDesde, DateTime fechaHasta, DateTime fechaAlta)
        {
            SetId(id);
            SetPaseadorId(paseadorId);
            SetPeriodo(fechaDesde, fechaHasta);
            SetFechaAlta(fechaAlta);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id no puede ser negativo.", nameof(id));
            Id = id;
        }

        public void SetPaseadorId(int paseadorId)
        {
            if (paseadorId <= 0)
                throw new ArgumentException("La liquidación debe tener un paseador asignado.", nameof(paseadorId));
            PaseadorId = paseadorId;
        }

        // Se guardan solo las fechas: el período abarca los días completos
        public void SetPeriodo(DateTime fechaDesde, DateTime fechaHasta)
        {
            if (fechaDesde == default || fechaHasta == default)
                throw new ArgumentException("El período debe tener fecha desde y fecha hasta.", nameof(fechaDesde));
            if (fechaDesde.Date > fechaHasta.Date)
                throw new ArgumentException("La fecha desde no puede ser posterior a la fecha hasta.", nameof(fechaDesde));
            FechaDesde = fechaDesde.Date;
            FechaHasta = fechaHasta.Date;
        }

        public void SetFechaAlta(DateTime fechaAlta)
        {
            if (fechaAlta == default)
                throw new ArgumentException("La fecha de alta no puede ser nula.", nameof(fechaAlta));
            FechaAlta = fechaAlta;
        }

        // El importe se copia del paseo y queda congelado, como el precio en una factura
        public void AgregarPaseo(Paseo paseo)
        {
            if (paseo.PaseadorId != PaseadorId)
                throw new ArgumentException($"El paseo {paseo.Id} no es del paseador de la liquidación.");

            if (paseo.FechaHoraInicio.Date < FechaDesde || paseo.FechaHoraInicio.Date > FechaHasta)
                throw new ArgumentException($"El paseo {paseo.Id} está fuera del período liquidado.");

            if (paseo.CalcularFechaHoraFin() > DateTime.Now)
                throw new ArgumentException($"El paseo {paseo.Id} todavía no terminó, no se puede liquidar.");

            if (_detalles.Any(d => d.PaseoId == paseo.Id))
                throw new ArgumentException($"El paseo {paseo.Id} ya está en la liquidación.");

            _detalles.Add(new LiquidacionDetalle(0, Id, paseo.Id, paseo.PrecioTotal));
        }

        public void QuitarPaseo(int paseoId)
        {
            _detalles.RemoveAll(d => d.PaseoId == paseoId);
        }

        // Una liquidación vacía no tiene sentido, como una factura sin renglones
        public void ValidarQueTengaDetalles()
        {
            if (_detalles.Count == 0)
                throw new ArgumentException("La liquidación tiene que incluir al menos un paseo.");
        }

        // Se calcula, no se guarda: el total siempre sale de sumar las líneas
        public decimal CalcularTotal()
        {
            return _detalles.Sum(d => d.Importe);
        }

        // Pasa a esta liquidación (la guardada) el período y las líneas de otra ya validada.
        // Las líneas que siguen se conservan, así no se pierde el importe congelado.
        public void ActualizarDesde(Liquidacion otra)
        {
            SetPaseadorId(otra.PaseadorId);
            SetPeriodo(otra.FechaDesde, otra.FechaHasta);

            var paseosNuevos = otra.Detalles.Select(d => d.PaseoId).ToList();
            _detalles.RemoveAll(d => !paseosNuevos.Contains(d.PaseoId));

            foreach (var detalle in otra.Detalles.Where(d => !_detalles.Any(e => e.PaseoId == d.PaseoId)))
            {
                _detalles.Add(new LiquidacionDetalle(0, Id, detalle.PaseoId, detalle.Importe));
            }
        }
    }
}
