using API.Clients;
using System.Net.Http.Headers;

namespace Paseadores.WinForms
{
    // Cliente compartido por todos los formularios: guarda la dirección de la API
    // y el token que devuelve el login.
    public static class ApiClient
    {
        // Es la URL del perfil "https" de la WebAPI (Properties/launchSettings.json)
        public const string UrlBase = "https://localhost:7140";

        private static readonly HttpClient _http = new HttpClient
        {
            BaseAddress = new Uri(UrlBase)
        };

        public static HttpClient Http => _http;
        // Clientes tipados: comparten el _http, así heredan el token automáticamente
        public static AuthApiClient Auth { get; } = new AuthApiClient(_http);
        public static DuenoApiClient Duenos { get; } = new DuenoApiClient(_http);
        public static PaseadorApiClient Paseadores { get; } = new PaseadorApiClient(_http);
        public static PerroApiClient Perros { get; } = new PerroApiClient(_http);
        public static PaseoApiClient Paseos { get; } = new PaseoApiClient(_http);
        public static LiquidacionApiClient Liquidaciones { get; } = new LiquidacionApiClient(_http);
        public static ReporteApiClient Reportes { get; } = new ReporteApiClient(_http);
        public static AdministradorApiClient Administradores { get; } = new AdministradorApiClient(_http);
        public static string Token { get; private set; } = string.Empty;

        public static void GuardarToken(string? token)
        {
            Token = token ?? string.Empty;

            _http.DefaultRequestHeaders.Authorization = string.IsNullOrWhiteSpace(Token)
                ? null
                : new AuthenticationHeaderValue("Bearer", Token);
        }

        // Sin token la API vuelve a responder 401
        public static void CerrarSesion()
        {
            GuardarToken(null);
        }
    }
}
