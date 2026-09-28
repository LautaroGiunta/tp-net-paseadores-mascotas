using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Clients
{
    public class PerroApiClient : BaseApiClient
    {
        public PerroApiClient(HttpClient http) : base(http) { }

        public async Task<List<PerroDTO>> GetAllAsync()
            => await GetListAsync<PerroDTO>("/perros");

        public async Task<PerroDTO?> GetAsync(int id)
            => await GetAsync<PerroDTO>($"/perros/{id}");

        // Recurso anidado: los perros de un dueño (el maestro/detalle)
        public async Task<List<PerroDTO>> GetByDuenoAsync(int duenoId)
            => await GetListAsync<PerroDTO>($"/duenos/{duenoId}/perros");

        public async Task<PerroDTO?> AddAsync(PerroDTO perro)
            => await PostAsync("/perros", perro);

        public async Task UpdateAsync(PerroDTO perro)
            => await PutAsync("/perros", perro);

        public async Task DeleteAsync(int id)
            => await DeleteAsync($"/perros/{id}");
    }
}
