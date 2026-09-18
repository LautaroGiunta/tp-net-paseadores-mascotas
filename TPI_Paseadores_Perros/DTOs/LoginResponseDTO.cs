namespace DTOs
{
    // Respuesta del login: el token para las llamadas siguientes y el usuario
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public UsuarioDTO Usuario { get; set; } = new UsuarioDTO();
    }
}
