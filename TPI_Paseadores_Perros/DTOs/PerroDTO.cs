namespace DTOs
{
    public class PerroDTO
    {
        public int Id { get; set; }
        public int DuenoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Raza { get; set; } = string.Empty;
        public int Edad { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
