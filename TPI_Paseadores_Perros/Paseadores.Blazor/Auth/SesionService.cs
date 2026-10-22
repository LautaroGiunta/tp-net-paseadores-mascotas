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
        private const string ClaveId = "usuario_id";

        private readonly IJSRuntime _js;
        private bool _cargada;

        public SesionService(IJSRuntime js)
        {
            _js = js;
        }

        public string? Nombre { get; private set; }
        public string? Rol { get; private set; }

        // Con el Id las páginas filtran lo que es del usuario (sus perros, sus paseos)
        public int Id { get; private set; }
        public bool EstaLogueado { get; private set; }

        public event Action? OnChange;

        public bool EsAdmin => Rol == "Admin";
        public bool EsDueno => Rol == "Dueno";
        public bool EsPaseador => Rol == "Paseador";

        public async Task CargarAsync()
        {
            if (_cargada)
                return;

            var token = await _js.InvokeAsync<string?>("localStorage.getItem", ClaveToken);
            Nombre = await _js.InvokeAsync<string?>("localStorage.getItem", ClaveNombre);
            Rol = await _js.InvokeAsync<string?>("localStorage.getItem", ClaveRol);
            var id = await _js.InvokeAsync<string?>("localStorage.getItem", ClaveId);
            Id = int.TryParse(id, out var idLeido) ? idLeido : 0;

            // Una sesión vieja sin Id guardado obliga a loguearse de nuevo
            EstaLogueado = !string.IsNullOrEmpty(token) && Id > 0;
            _cargada = true;
        }

        public async Task IniciarAsync(LoginResponseDTO respuesta)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", ClaveToken, respuesta.Token);
            await _js.InvokeVoidAsync("localStorage.setItem", ClaveNombre, respuesta.Usuario.Nombre);
            await _js.InvokeVoidAsync("localStorage.setItem", ClaveRol, respuesta.Usuario.Rol);
            await _js.InvokeVoidAsync("localStorage.setItem", ClaveId, respuesta.Usuario.Id.ToString());

            Nombre = respuesta.Usuario.Nombre;
            Rol = respuesta.Usuario.Rol;
            Id = respuesta.Usuario.Id;
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
            await _js.InvokeVoidAsync("localStorage.removeItem", ClaveId);

            Nombre = null;
            Rol = null;
            Id = 0;
            EstaLogueado = false;
            OnChange?.Invoke();
        }
    }
}
