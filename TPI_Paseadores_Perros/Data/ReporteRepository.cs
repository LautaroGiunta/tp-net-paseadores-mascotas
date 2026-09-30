using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    // Los reportes van con ADO.NET puro (SqlConnection + SqlCommand + SqlDataReader):
    // son consultas de solo lectura con agrupaciones.
    // La cadena de conexión se toma del contexto de EF para no configurarla dos veces.
    public class ReporteRepository : IReporteRepository
    {
        private readonly string _connectionString;

        public ReporteRepository(PaseadoresContext context)
        {
            _connectionString = context.Database.GetConnectionString()
                ?? throw new InvalidOperationException("El contexto no tiene cadena de conexión.");
        }

        // Solo cuentan los paseos que ya terminaron: los agendados todavía no se cobraron
        public async Task<IEnumerable<RecaudacionMensualItem>> GetRecaudacionMensualAsync(DateTime fechaDesde, DateTime fechaHasta)
        {
            const string sql = @"
                SELECT YEAR(p.FechaHoraInicio)  AS Anio,
                       MONTH(p.FechaHoraInicio) AS Mes,
                       p.PaseadorId,
                       u.Nombre,
                       u.Apellido,
                       COUNT(*)                 AS CantidadPaseos,
                       SUM(p.DuracionMinutos)   AS Minutos,
                       SUM(p.PrecioTotal)       AS Total
                FROM Paseos p
                INNER JOIN Usuarios u ON u.Id = p.PaseadorId
                WHERE p.FechaHoraInicio >= @desde
                  AND p.FechaHoraInicio <  @hastaExclusivo
                  AND DATEADD(MINUTE, p.DuracionMinutos, p.FechaHoraInicio) <= @ahora
                GROUP BY YEAR(p.FechaHoraInicio), MONTH(p.FechaHoraInicio), p.PaseadorId, u.Nombre, u.Apellido
                ORDER BY Anio, Mes, p.PaseadorId";

            var items = new List<RecaudacionMensualItem>();

            using var conexion = new SqlConnection(_connectionString);
            using var comando = new SqlCommand(sql, conexion);
            AgregarPeriodo(comando, fechaDesde, fechaHasta);

            await conexion.OpenAsync();
            using var lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                items.Add(new RecaudacionMensualItem
                {
                    Anio = lector.GetInt32(lector.GetOrdinal("Anio")),
                    Mes = lector.GetInt32(lector.GetOrdinal("Mes")),
                    PaseadorId = lector.GetInt32(lector.GetOrdinal("PaseadorId")),
                    PaseadorNombre = lector.GetString(lector.GetOrdinal("Nombre")),
                    PaseadorApellido = lector.GetString(lector.GetOrdinal("Apellido")),
                    CantidadPaseos = lector.GetInt32(lector.GetOrdinal("CantidadPaseos")),
                    Minutos = lector.GetInt32(lector.GetOrdinal("Minutos")),
                    Total = lector.GetDecimal(lector.GetOrdinal("Total"))
                });
            }

            return items;
        }

        // LEFT JOIN: los perros sin paseos en el período también aparecen, con cero
        public async Task<IEnumerable<ActividadPerroItem>> GetActividadPerrosAsync(DateTime fechaDesde, DateTime fechaHasta)
        {
            const string sql = @"
                SELECT pe.Id                            AS PerroId,
                       pe.Nombre                        AS PerroNombre,
                       pe.Raza,
                       u.Nombre                         AS DuenoNombre,
                       u.Apellido                       AS DuenoApellido,
                       COUNT(pa.Id)                     AS CantidadPaseos,
                       COALESCE(SUM(pa.DuracionMinutos), 0) AS Minutos,
                       COALESCE(SUM(pa.PrecioTotal), 0)     AS Total,
                       MAX(pa.FechaHoraInicio)          AS UltimoPaseo
                FROM Perros pe
                INNER JOIN Usuarios u ON u.Id = pe.DuenoId
                LEFT JOIN Paseos pa ON pa.PerroId = pe.Id
                                   AND pa.FechaHoraInicio >= @desde
                                   AND pa.FechaHoraInicio <  @hastaExclusivo
                                   AND DATEADD(MINUTE, pa.DuracionMinutos, pa.FechaHoraInicio) <= @ahora
                GROUP BY pe.Id, pe.Nombre, pe.Raza, u.Nombre, u.Apellido
                ORDER BY Total DESC, pe.Nombre";

            var items = new List<ActividadPerroItem>();

            using var conexion = new SqlConnection(_connectionString);
            using var comando = new SqlCommand(sql, conexion);
            AgregarPeriodo(comando, fechaDesde, fechaHasta);

            await conexion.OpenAsync();
            using var lector = await comando.ExecuteReaderAsync();

            int colUltimo = lector.GetOrdinal("UltimoPaseo");
            while (await lector.ReadAsync())
            {
                items.Add(new ActividadPerroItem
                {
                    PerroId = lector.GetInt32(lector.GetOrdinal("PerroId")),
                    PerroNombre = lector.GetString(lector.GetOrdinal("PerroNombre")),
                    Raza = lector.GetString(lector.GetOrdinal("Raza")),
                    DuenoNombre = lector.GetString(lector.GetOrdinal("DuenoNombre")),
                    DuenoApellido = lector.GetString(lector.GetOrdinal("DuenoApellido")),
                    CantidadPaseos = lector.GetInt32(lector.GetOrdinal("CantidadPaseos")),
                    Minutos = lector.GetInt32(lector.GetOrdinal("Minutos")),
                    Total = lector.GetDecimal(lector.GetOrdinal("Total")),
                    UltimoPaseo = lector.IsDBNull(colUltimo) ? null : lector.GetDateTime(colUltimo)
                });
            }

            return items;
        }

        // Parámetros siempre con SqlParameter, nunca concatenando texto (evita inyección SQL).
        // El "hasta" se toma como día completo.
        private static void AgregarPeriodo(SqlCommand comando, DateTime fechaDesde, DateTime fechaHasta)
        {
            comando.Parameters.Add("@desde", System.Data.SqlDbType.DateTime2).Value = fechaDesde.Date;
            comando.Parameters.Add("@hastaExclusivo", System.Data.SqlDbType.DateTime2).Value = fechaHasta.Date.AddDays(1);
            comando.Parameters.Add("@ahora", System.Data.SqlDbType.DateTime2).Value = DateTime.Now;
        }
    }
}
