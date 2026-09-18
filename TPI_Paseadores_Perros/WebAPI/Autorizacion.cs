namespace WebAPI
{
    // Los nombres de los roles son los del enum RolUsuario y viajan dentro del token
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Dueno = "Dueno";
        public const string Paseador = "Paseador";
    }

    // Se registran en Program.cs y se aplican con RequireAuthorization(...)
    public static class Politicas
    {
        // Los usuarios del sistema los administra solo el Admin
        public const string SoloAdmin = "SoloAdmin";

        // Los perros los carga el Admin o el dueño que los tiene a cargo
        public const string AdminODueno = "AdminODueno";

        // El paseo lo pide el dueño, lo registra el paseador y lo corrige el Admin
        public const string GestionDePaseos = "GestionDePaseos";
    }
}
