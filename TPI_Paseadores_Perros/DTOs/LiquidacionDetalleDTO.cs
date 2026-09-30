namespace DTOs
{
    // Para guardar alcanza con el PaseoId; el resto lo completa la API al devolverla
    public class LiquidacionDetalleDTO
    {
        public int Id { get; set; }
        public int PaseoId { get; set; }
        public decimal Importe { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public int DuracionMinutos { get; set; }
        public string PerroNombre { get; set; } = string.Empty;
    }
}
