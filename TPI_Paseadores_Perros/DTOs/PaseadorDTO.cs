namespace DTOs
{
    public class PaseadorDTO : UsuarioDTO
    {
        public string Zona { get; set; } = string.Empty;
        public decimal TarifaPorHora { get; set; }
    }
}
