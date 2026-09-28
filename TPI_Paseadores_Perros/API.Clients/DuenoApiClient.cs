using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Clients
{
    public class DuenoApiClient : BaseApiClient
    {
        public DuenoApiClient(HttpClient http) : base(http) { }

        public async Task<List<DuenoDTO>> GetAllAsync()
            => await GetListAsync<DuenoDTO>("/duenos");

        public async Task<DuenoDTO?> GetAsync(int id)
            => await GetAsync<DuenoDTO>($"/duenos/{id}");

        public async Task<DuenoDTO?> AddAsync(DuenoDTO dueno)
            => await PostAsync("/duenos", dueno);

        public async Task UpdateAsync(DuenoDTO dueno)
            => await PutAsync("/duenos", dueno);

        public async Task DeleteAsync(int id)
            => await DeleteAsync($"/duenos/{id}");
    }
}
