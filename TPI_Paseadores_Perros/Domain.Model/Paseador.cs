using System.Text.RegularExpressions;

namespace Domain.Model
{
    public class Paseador : Usuario
    {
        public string Zona { get; private set; }
        public decimal TarifaPorHora { get; private set; }

        public Paseador(int id, string nombre, string apellido, string email,
                        string telefono, string zona, decimal tarifaPorHora,string contrasena, DateTime fechaAlta)
            :base(id, nombre, apellido, email, telefono, contrasena, RolUsuario.Paseador, fechaAlta)
        {
            SetZona(zona);
            SetTarifaPorHora(tarifaPorHora);
        }

        public void SetZona(string zona)
        {
            if (string.IsNullOrWhiteSpace(zona))
                throw new ArgumentException("La zona no puede ser nula o vacía.", nameof(zona));
            Zona = zona.Trim();
        }

        public void SetTarifaPorHora(decimal tarifaPorHora)
        {
            if (tarifaPorHora <= 0)
                throw new ArgumentException("La tarifa por hora debe ser mayor que 0.", nameof(tarifaPorHora));
            TarifaPorHora = tarifaPorHora;
        }

    }
}
