namespace DTOs
{
    public class PaseadorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Zona { get; set; } = string.Empty;
        public decimal TarifaPorHora { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
