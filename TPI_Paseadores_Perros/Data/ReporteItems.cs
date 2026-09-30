namespace Data
{
    // Filas que devuelven las consultas de reportes (solo lectura, no son entidades de EF)

    // Lo recaudado por un paseador en un mes, con los paseos ya realizados
    public class RecaudacionMensualItem
    {
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int PaseadorId { get; set; }
        public string PaseadorNombre { get; set; } = string.Empty;
        public string PaseadorApellido { get; set; } = string.Empty;
        public int CantidadPaseos { get; set; }
        public int Minutos { get; set; }
        public decimal Total { get; set; }
    }

    // Cuánto paseó cada perro en el período (incluye los que no salieron nunca)
    public class ActividadPerroItem
    {
        public int PerroId { get; set; }
        public string PerroNombre { get; set; } = string.Empty;
        public string Raza { get; set; } = string.Empty;
        public string DuenoNombre { get; set; } = string.Empty;
        public string DuenoApellido { get; set; } = string.Empty;
        public int CantidadPaseos { get; set; }
        public int Minutos { get; set; }
        public decimal Total { get; set; }
        public DateTime? UltimoPaseo { get; set; }
    }
}
