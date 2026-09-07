namespace Domain.Model
{
    public class Paseo
    {
        public int Id { get; private set; }
        public int PaseadorId { get; private set; }
        public int PerroId { get; private set; }
        public DateTime FechaHoraInicio { get; private set; }
        public int DuracionMinutos { get; private set; }
        public decimal PrecioTotal { get; private set; }
        public DateTime FechaAlta { get; private set; }

        public Paseo(int id, int paseadorId, int perroId, DateTime fechaHoraInicio,
                     int duracionMinutos, decimal precioTotal, DateTime fechaAlta)
        {
            SetId(id);
            SetPaseadorId(paseadorId);
            SetPerroId(perroId);
            SetFechaHoraInicio(fechaHoraInicio);
            SetDuracionMinutos(duracionMinutos);
            SetPrecioTotal(precioTotal);
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
                throw new ArgumentException("El paseo debe tener un paseador asignado.", nameof(paseadorId));
            PaseadorId = paseadorId;
        }

        public void SetPerroId(int perroId)
        {
            if (perroId <= 0)
                throw new ArgumentException("El paseo debe tener un perro asignado.", nameof(perroId));
            PerroId = perroId;
        }

        public void SetFechaHoraInicio(DateTime fechaHoraInicio)
        {
            if (fechaHoraInicio == default)
                throw new ArgumentException("La fecha y hora de inicio no puede ser nula.", nameof(fechaHoraInicio));
            FechaHoraInicio = fechaHoraInicio;
        }

        public void SetDuracionMinutos(int duracionMinutos)
        {
            if (duracionMinutos < 15 || duracionMinutos > 480)
                throw new ArgumentException("La duración debe estar entre 15 y 480 minutos.", nameof(duracionMinutos));
            DuracionMinutos = duracionMinutos;
        }

        public void SetPrecioTotal(decimal precioTotal)
        {
            if (precioTotal <= 0)
                throw new ArgumentException("El precio total debe ser mayor que 0.", nameof(precioTotal));
            PrecioTotal = precioTotal;
        }

        public void SetFechaAlta(DateTime fechaAlta)
        {
            if (fechaAlta == default)
                throw new ArgumentException("La fecha de alta no puede ser nula.", nameof(fechaAlta));
            FechaAlta = fechaAlta;
        }

        // Se calcula, no se guarda: el fin siempre se deduce del inicio y la duración.
        public DateTime CalcularFechaHoraFin()
        {
            return FechaHoraInicio.AddMinutes(DuracionMinutos);
        }
    }
}
