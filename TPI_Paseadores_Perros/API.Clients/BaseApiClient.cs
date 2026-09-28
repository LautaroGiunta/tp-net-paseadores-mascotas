using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace API.Clients
{
    // Clase base: envuelve el HttpClient y centraliza las llamadas y los errores.
    // 'abstract' = no se usa sola, solo para heredar de ella.
    public abstract class BaseApiClient
    {
        protected readonly HttpClient _http;

        protected BaseApiClient(HttpClient http)
        {
            _http = http;
        }

        // --- Helpers genéricos: sirven para CUALQUIER entidad ---

        protected async Task<List<T>> GetListAsync<T>(string url)
        {
            var response = await _http.GetAsync(url);
            await GarantizarExitoAsync(response);
            return await response.Content.ReadFromJsonAsync<List<T>>() ?? new List<T>();
        }

        protected async Task<T?> GetAsync<T>(string url)
        {
            var response = await _http.GetAsync(url);
            await GarantizarExitoAsync(response);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        protected async Task<T?> PostAsync<T>(string url, T dto)
        {
            var response = await _http.PostAsJsonAsync(url, dto);
            await GarantizarExitoAsync(response);
            return await response.Content.ReadFromJsonAsync<T>();
        }
        // POST donde lo que mando y lo que recibo son tipos distintos (ej: login)
        protected async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest dto)
        {
            var response = await _http.PostAsJsonAsync(url, dto);
            await GarantizarExitoAsync(response);
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        protected async Task PutAsync<T>(string url, T dto)
        {
            var response = await _http.PutAsJsonAsync(url, dto);
            await GarantizarExitoAsync(response);
        }

        protected async Task DeleteAsync(string url)
        {
            var response = await _http.DeleteAsync(url);
            await GarantizarExitoAsync(response);
        }

        // --- El manejo de errores, en UN solo lugar ---
        // Si la respuesta no fue exitosa, lee el mensaje de la API y lanza ApiException.
        private static async Task GarantizarExitoAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return;

            string cuerpo = await response.Content.ReadAsStringAsync();
            string mensaje = ExtraerMensaje(cuerpo, response.StatusCode);
            throw new ApiException((int)response.StatusCode, mensaje);
        }

        // La API manda { "error": "..." } o { "mensaje": "..." }; sacamos ese texto.
        private static string ExtraerMensaje(string cuerpo, System.Net.HttpStatusCode status)
        {
            try
            {
                using var doc = System.Text.Json.JsonDocument.Parse(cuerpo);
                if (doc.RootElement.TryGetProperty("error", out var e))
                    return e.GetString() ?? cuerpo;
                if (doc.RootElement.TryGetProperty("mensaje", out var m))
                    return m.GetString() ?? cuerpo;
            }
            catch
            {
                // el cuerpo no era JSON: lo devolvemos tal cual
            }

            return string.IsNullOrWhiteSpace(cuerpo) ? $"Error {(int)status}" : cuerpo;
        }
    }
}
