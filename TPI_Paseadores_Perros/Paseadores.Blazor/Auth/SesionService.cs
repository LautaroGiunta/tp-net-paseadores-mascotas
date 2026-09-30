using DTOs;
using Microsoft.JSInterop;

namespace Paseadores.Blazor.Auth
{
    // Guarda quién inició sesión (en localStorage, para que sobreviva a un F5)
    // y avisa a la barra y al menú cuando cambia, así se redibujan sin recargar.
    public class SesionService
    {
        // AuthTokenHandler lee la misma clave del token en cada llamada a la API
        public const string ClaveToken = "token_seguridad";
        private const string ClaveNombre = "usuario_nombre";
        private const string ClaveRol = "usuario_rol";

        private readonly IJSRuntime _js;
        private bool _cargada;

        public SesionService(IJSRuntime js)
        {
            _js = js;
        }

        public string? Nombre { get; private set; }
        public string? Rol { get; private set; }
        public bool EstaLogueado { get; private set; }

        public event Action? OnChange;

        public bool EsAdmin => Rol == "Admin";
        public bool EsDueno => Rol == "Dueno";

        public async Task CargarAsync()
        {
            if (_cargada)
                return;

            var token = await _js.InvokeAsync<string?>("localStorage.getItem", ClaveToken);
            Nombre = await _js.InvokeAsync<string?>("localStorage.getItem", ClaveNombre);
            Rol = await _js.InvokeAsync<string?>("localStorage.getItem", ClaveRol);
            EstaLogueado = !string.IsNullOrEmpty(token);
            _cargada = true;
        }

        public async Task IniciarAsync(LoginResponseDTO respuesta)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", ClaveToken, respuesta.Token);
            await _js.InvokeVoidAsync("localStorage.setItem", ClaveNombre, respuesta.Usuario.Nombre);
            await _js.InvokeVoidAsync("localStorage.setItem", ClaveRol, respuesta.Usuario.Rol);

            Nombre = respuesta.Usuario.Nombre;
            Rol = respuesta.Usuario.Rol;
            EstaLogueado = true;
            _cargada = true;
            OnChange?.Invoke();
        }

        // Sin token la API vuelve a responder 401
        public async Task CerrarAsync()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", ClaveToken);
            await _js.InvokeVoidAsync("localStorage.removeItem", ClaveNombre);
            await _js.InvokeVoidAsync("localStorage.removeItem", ClaveRol);

            Nombre = null;
            Rol = null;
            EstaLogueado = false;
            OnChange?.Invoke();
        }
    }
}
