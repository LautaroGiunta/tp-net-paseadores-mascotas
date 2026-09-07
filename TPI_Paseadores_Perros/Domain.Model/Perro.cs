namespace Domain.Model
{
    public class Perro
    {
        public int Id { get; private set; }
        public int DuenoId { get; private set; }
        public string Nombre { get; private set; }
        public string Raza { get; private set; }
        public int Edad { get; private set; }
        public DateTime FechaAlta { get; private set; }

        public Perro(int id, int duenoId, string nombre, string raza,
                     int edad, DateTime fechaAlta)
        {
            SetId(id);
            SetDuenoId(duenoId);
            SetNombre(nombre);
            SetRaza(raza);
            SetEdad(edad);
            SetFechaAlta(fechaAlta);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id no puede ser negativo.", nameof(id));
            Id = id;
        }

        public void SetDuenoId(int duenoId)
        {
            if (duenoId <= 0)
                throw new ArgumentException("El perro debe tener un dueño asignado.", nameof(duenoId));
            DuenoId = duenoId;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombre));
            Nombre = nombre.Trim();
        }

        public void SetRaza(string raza)
        {
            if (string.IsNullOrWhiteSpace(raza))
                throw new ArgumentException("La raza no puede ser nula o vacía.", nameof(raza));
            Raza = raza.Trim();
        }

        public void SetEdad(int edad)
        {
            if (edad < 0 || edad > 30)
                throw new ArgumentException("La edad debe estar entre 0 y 30 años.", nameof(edad));
            Edad = edad;
        }

        public void SetFechaAlta(DateTime fechaAlta)
        {
            if (fechaAlta == default)
                throw new ArgumentException("La fecha de alta no puede ser nula.", nameof(fechaAlta));
            FechaAlta = fechaAlta;
        }
    }
}
