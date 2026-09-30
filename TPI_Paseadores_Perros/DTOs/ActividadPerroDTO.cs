namespace DTOs
{
    public class ActividadPerroDTO
    {
        public int PerroId { get; set; }
        public string PerroNombre { get; set; } = string.Empty;
        public string Raza { get; set; } = string.Empty;
        public string DuenoNombre { get; set; } = string.Empty;
        public int CantidadPaseos { get; set; }
        public int Minutos { get; set; }
        public decimal Total { get; set; }
        public DateTime? UltimoPaseo { get; set; }
    }
}
