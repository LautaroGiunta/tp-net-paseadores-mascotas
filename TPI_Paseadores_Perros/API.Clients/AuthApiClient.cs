using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Clients
{
    public class AuthApiClient : BaseApiClient
    {
        public AuthApiClient(HttpClient http) : base(http) { }

        public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO login)
            => await PostAsync<LoginRequestDTO, LoginResponseDTO>("/api/auth/login", login);
    }
}
