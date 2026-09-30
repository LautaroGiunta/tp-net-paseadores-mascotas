namespace DTOs
{
    public class RecaudacionMensualDTO
    {
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int PaseadorId { get; set; }
        public string PaseadorNombre { get; set; } = string.Empty;
        public int CantidadPaseos { get; set; }
        public int Minutos { get; set; }
        public decimal Total { get; set; }
    }
}
