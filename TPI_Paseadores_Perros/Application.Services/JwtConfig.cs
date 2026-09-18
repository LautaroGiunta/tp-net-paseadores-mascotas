namespace Application.Services
{
    // Se carga desde la sección "Jwt" del appsettings de la WebAPI
    public class JwtConfig
    {
        public string ClaveSecreta { get; set; } = string.Empty;
        public string Emisor { get; set; } = string.Empty;
        public string Audiencia { get; set; } = string.Empty;
        public int MinutosDeExpiracion { get; set; } = 60;
    }
}
