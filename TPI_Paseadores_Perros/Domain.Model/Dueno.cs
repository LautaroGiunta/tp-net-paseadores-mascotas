using System.Text.RegularExpressions;

namespace Domain.Model
{
    public class Dueno : Usuario
    {
        public string Direccion { get; private set; }

        public Dueno(int id, string nombre, string apellido, string email,
                     string telefono, string direccion,string contrasena, DateTime fechaAlta)
            : base(id, nombre, apellido, email, telefono, contrasena, RolUsuario.Dueno, fechaAlta)
        {
            SetDireccion(direccion);
        }


        public void SetDireccion(string direccion)
        {
            if (string.IsNullOrWhiteSpace(direccion))
                throw new ArgumentException("La dirección no puede ser nula o vacía.", nameof(direccion));
            Direccion = direccion.Trim();
        }

    }
}
