namespace Domain.Model
{
    // Una línea de la liquidación: un paseo que se le paga al paseador.
    // No tiene vida propia, se crea y se borra junto con su Liquidacion.
    public class LiquidacionDetalle
    {
        public int Id { get; private set; }
        public int LiquidacionId { get; private set; }
        public int PaseoId { get; private set; }
        public decimal Importe { get; private set; }
        public Paseo? Paseo { get; private set; }

        public LiquidacionDetalle(int id, int liquidacionId, int paseoId, decimal importe)
        {
            SetId(id);
            SetLiquidacionId(liquidacionId);
            SetPaseoId(paseoId);
            SetImporte(importe);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id no puede ser negativo.", nameof(id));
            Id = id;
        }

        // En 0 mientras la cabecera no se guardó: EF la completa al insertar
        public void SetLiquidacionId(int liquidacionId)
        {
            if (liquidacionId < 0)
                throw new ArgumentException("El Id de la liquidación no puede ser negativo.", nameof(liquidacionId));
            LiquidacionId = liquidacionId;
        }

        public void SetPaseoId(int paseoId)
        {
            if (paseoId <= 0)
                throw new ArgumentException("La línea debe tener un paseo asignado.", nameof(paseoId));
            PaseoId = paseoId;
        }

        public void SetImporte(decimal importe)
        {
            if (importe <= 0)
                throw new ArgumentException("El importe debe ser mayor que 0.", nameof(importe));
            Importe = importe;
        }
    }
}
