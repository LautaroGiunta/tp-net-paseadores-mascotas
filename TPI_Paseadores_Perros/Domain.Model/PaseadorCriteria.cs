namespace Domain.Model
{
    public class PaseadorCriteria
    {
        public string Texto { get; private set; }

        public PaseadorCriteria(string texto)
        {
            Texto = (texto ?? string.Empty).Trim();
        }
    }
}
