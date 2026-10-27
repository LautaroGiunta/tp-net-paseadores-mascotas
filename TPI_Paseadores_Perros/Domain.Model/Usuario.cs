using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Usuario
    {
        // También lo usa PaseadoresContext para el largo de la columna Email
        public const int LargoMaximoEmail = 256;

        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public string Email { get; private set; }
        public string Telefono { get; private set; }
        public string Contrasena { get; private set; } 
        public RolUsuario Rol { get; private set; }    
        public DateTime FechaAlta { get; private set; }

        // Constructor vacío para Entity Framework
        protected Usuario() { }

        // Constructor para cuando creamos el usuario desde el código
        public Usuario(int id, string nombre, string apellido, string email, string telefono, string contrasena, RolUsuario rol, DateTime fechaAlta)
        {
            SetId(id);
            SetNombre(nombre);
            SetApellido(apellido);
            SetEmail(email);
            SetTelefono(telefono);
            SetContrasena(contrasena);
            Rol = rol;
            SetFechaAlta(fechaAlta);
        }

        // Métodos para actualizar datos compartidos
        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id no puede ser negativo.", nameof(id));
            Id = id;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombre));
            Nombre = nombre.Trim();
        }

        public void SetApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido no puede ser nulo o vacío.", nameof(apellido));
            Apellido = apellido.Trim();
        }
        public void SetEmail(string email)
        {
            if (!EsEmailValido(email))
                throw new ArgumentException("El email no tiene un formato válido.", nameof(email));
            if (email.Trim().Length > LargoMaximoEmail)
                throw new ArgumentException($"El email no puede tener más de {LargoMaximoEmail} caracteres.", nameof(email));
            Email = email.Trim();
        }
        private static bool EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
        public void SetTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                throw new ArgumentException("El teléfono no puede ser nulo o vacío.", nameof(telefono));
            Telefono = telefono.Trim();
        }

        // Valida la contraseña en texto plano, antes de hashearla
        public static void ValidarContrasena(string contrasena)
        {
            if (string.IsNullOrWhiteSpace(contrasena))
                throw new ArgumentException("La contraseña no puede ser nula o vacía.", nameof(contrasena));
            if (contrasena.Length < 8)
                throw new ArgumentException("La contraseña tiene que tener un minimo de 8 caracteres.", nameof(contrasena));
        }

        // Recibe la contraseña ya hasheada
        public void SetContrasena(string contrasenaHasheada)
        {
            if (string.IsNullOrWhiteSpace(contrasenaHasheada))
                throw new ArgumentException("La contraseña no puede ser nula o vacía.", nameof(contrasenaHasheada));
            Contrasena = contrasenaHasheada;
        }
        public void SetFechaAlta(DateTime fechaAlta)
        {
            if (fechaAlta == default)
                throw new ArgumentException("La fecha de alta no puede ser nula.", nameof(fechaAlta));
            FechaAlta = fechaAlta;
        }
    }
}
