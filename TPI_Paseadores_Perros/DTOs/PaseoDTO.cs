namespace DTOs
{
    public class PaseoDTO
    {
        public int Id { get; set; }
        public int PaseadorId { get; set; }
        public int PerroId { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public int DuracionMinutos { get; set; }
        public decimal PrecioTotal { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
