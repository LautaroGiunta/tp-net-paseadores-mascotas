namespace DTOs
{
    public class LiquidacionDTO
    {
        public int Id { get; set; }
        public int PaseadorId { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public DateTime FechaAlta { get; set; }
        public decimal Total { get; set; }
        public string PaseadorNombre { get; set; } = string.Empty;
        public List<LiquidacionDetalleDTO> Detalles { get; set; } = new List<LiquidacionDetalleDTO>();
    }
}
