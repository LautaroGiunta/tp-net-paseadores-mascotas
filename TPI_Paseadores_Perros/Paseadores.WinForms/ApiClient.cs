using API.Clients;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace Paseadores.WinForms
{
    // Cliente compartido por todos los formularios: guarda la dirección de la API
    // y el token que devuelve el login.
    public static class ApiClient
    {
        private static readonly IConfiguration _config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
        
        // URL de la API, sale de appsettings.json (clave UrlApi)
        public static readonly string UrlBase = _config["UrlApi"]!;

        // Evita que dos llamadas que fallan juntas muestren el aviso dos veces
        private static bool _sesionVencida = false;

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
            _sesionVencida = false;

            _http.DefaultRequestHeaders.Authorization = string.IsNullOrWhiteSpace(Token)
                ? null
                : new AuthenticationHeaderValue("Bearer", Token);
        }

        // Sin token la API vuelve a responder 401
        public static void CerrarSesion()
        {
            GuardarToken(null);
        }

        // La API respondió 401: el token venció. Avisa y vuelve al login,
        // igual que "Cerrar sesión" del menú.
        public static void SesionVencida()
        {
            if (_sesionVencida) return;
            _sesionVencida = true;

            MessageBox.Show("Tu sesión venció. Volvé a iniciar sesión.",
                "Sesión vencida", MessageBoxButtons.OK, MessageBoxIcon.Information);

            FormMain? main = Application.OpenForms.OfType<FormMain>().FirstOrDefault();
            if (main != null)
            {
                main.QuiereCerrarSesion = true;
                main.Close();   // cierra también las ventanas hijas (son MDI)
            }
        }
    }
}
