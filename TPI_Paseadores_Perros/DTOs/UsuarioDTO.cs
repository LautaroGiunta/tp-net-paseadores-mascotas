namespace DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        // Solo se usa para el alta y la modificación, los servicios no la devuelven
        public string Contrasena { get; set; } = string.Empty;

        public string Rol { get; set; } = string.Empty;
        public DateTime FechaAlta { get; set; }
    }
}
