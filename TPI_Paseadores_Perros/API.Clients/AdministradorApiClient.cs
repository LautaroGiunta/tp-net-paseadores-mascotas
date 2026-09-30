using DTOs;

namespace API.Clients
{
    public class AdministradorApiClient : BaseApiClient
    {
        public AdministradorApiClient(HttpClient http) : base(http) { }

        public async Task<List<UsuarioDTO>> GetAllAsync()
            => await GetListAsync<UsuarioDTO>("/administradores");

        public async Task<UsuarioDTO?> GetAsync(int id)
            => await GetAsync<UsuarioDTO>($"/administradores/{id}");

        public async Task<UsuarioDTO?> AddAsync(UsuarioDTO administrador)
            => await PostAsync("/administradores", administrador);

        public async Task UpdateAsync(UsuarioDTO administrador)
            => await PutAsync("/administradores", administrador);

        public async Task DeleteAsync(int id)
            => await DeleteAsync($"/administradores/{id}");
    }
}
