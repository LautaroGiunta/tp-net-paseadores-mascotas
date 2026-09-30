using Domain.Model;

namespace Data
{
    // Datos de prueba para poder usar y mostrar el sistema entero: dueños, paseadores,
    // perros, paseos (hechos y agendados) y liquidaciones. Solo se cargan si la base
    // no tiene perros ni paseos, así no se mezclan con datos cargados a mano.
    // Las fechas son relativas a hoy para que siempre haya paseos pasados y futuros.
    public static class DatosDePrueba
    {
        public static void CargarSiHaceFalta(PaseadoresContext context)
        {
            if (context.Perros.Any() || context.Paseos.Any())
                return;

            using var transaccion = context.Database.BeginTransaction();

            var ahora = DateTime.Now;
            var hoy = DateTime.Today;
            var alta = hoy.AddMonths(-4);

            // Ana Gómez (Id 2) y Carlos Ruiz (Id 3) ya vienen de la migración SeedUsuariosIniciales
            var ana = context.Duenos.Find(2)!;
            var carlos = context.Paseadores.Find(3)!;

            var martin = new Dueno(0, "Martín", "López", "martin.lopez@paseos.com", "3415550103", "Oroño 845", "Dueno123", alta);
            var lucia = new Dueno(0, "Lucía", "Fernández", "lucia.fernandez@paseos.com", "3415550104", "Pellegrini 2210", "Dueno123", alta);
            var jorge = new Dueno(0, "Jorge", "Pérez", "jorge.perez@paseos.com", "3415550105", "Mendoza 3120", "Dueno123", alta);
            context.Duenos.AddRange(martin, lucia, jorge);

            var sofia = new Paseador(0, "Sofía", "Martínez", "sofia.martinez@paseos.com", "3415550106", "Norte", 4000m, "Paseo123", alta);
            var diego = new Paseador(0, "Diego", "Suárez", "diego.suarez@paseos.com", "3415550107", "Oeste", 3000m, "Paseo123", alta);
            context.Paseadores.AddRange(sofia, diego);
            context.SaveChanges();

            var perros = new List<Perro>
            {
                new Perro(0, ana.Id, "Luna", "Golden Retriever", 4, alta),
                new Perro(0, ana.Id, "Rocco", "Beagle", 2, alta),
                new Perro(0, martin.Id, "Toby", "Labrador", 6, alta),
                new Perro(0, lucia.Id, "Mora", "Caniche", 9, alta),
                new Perro(0, lucia.Id, "Simón", "Bulldog Francés", 3, alta),
                new Perro(0, jorge.Id, "Kira", "Pastor Alemán", 5, alta),
                new Perro(0, jorge.Id, "Pancho", "Mestizo", 11, alta)
            };
            context.Perros.AddRange(perros);
            context.SaveChanges();

            // Cada paseador pasea día por medio, a una hora que va rotando: nunca se le pisan dos paseos.
            // Random con semilla fija: los datos salen siempre iguales.
            var paseadores = new List<Paseador> { carlos, sofia, diego };
            var horas = new[] { 9, 11, 17 };
            var duraciones = new[] { 30, 45, 60, 60, 90 };
            var azar = new Random(2026);
            var paseos = new List<Paseo>();

            for (int dia = -90; dia <= 14; dia++)
            {
                for (int i = 0; i < paseadores.Count; i++)
                {
                    if ((dia + i) % 2 != 0)
                        continue;

                    var inicio = hoy.AddDays(dia).AddHours(horas[Math.Abs(dia + i) % horas.Length]);
                    int duracion = duraciones[azar.Next(duraciones.Length)];
                    var perro = perros[azar.Next(perros.Count)];
                    var paseador = paseadores[i];

                    // Los pasados se dieron de alta unos días antes, como pasaría en la realidad
                    var fechaAlta = inicio < ahora ? inicio.AddDays(-3) : ahora;

                    paseos.Add(new Paseo(0, paseador.Id, perro.Id, inicio, duracion,
                                         paseador.CalcularPrecio(duracion), fechaAlta));
                }
            }
            context.Paseos.AddRange(paseos);
            context.SaveChanges();

            // Liquidados: los meses anteriores al mes pasado. El mes pasado y el actual quedan
            // pendientes, para poder mostrar cómo se arma una liquidación nueva.
            var inicioMesPasado = new DateTime(hoy.Year, hoy.Month, 1).AddMonths(-1);
            var meses = paseos
                .Where(p => p.FechaHoraInicio < inicioMesPasado)
                .Select(p => new DateTime(p.FechaHoraInicio.Year, p.FechaHoraInicio.Month, 1))
                .Distinct()
                .OrderBy(m => m);

            foreach (var mes in meses)
            {
                foreach (var paseador in paseadores)
                {
                    var delMes = paseos
                        .Where(p => p.PaseadorId == paseador.Id &&
                                    p.FechaHoraInicio >= mes && p.FechaHoraInicio < mes.AddMonths(1))
                        .ToList();

                    if (delMes.Count == 0)
                        continue;

                    var liquidacion = new Liquidacion(0, paseador.Id, mes, mes.AddMonths(1).AddDays(-1), mes.AddMonths(1).AddDays(2));
                    foreach (var paseo in delMes)
                    {
                        liquidacion.AgregarPaseo(paseo);
                    }
                    context.Liquidaciones.Add(liquidacion);
                }
            }
            context.SaveChanges();

            transaccion.Commit();
        }
    }
}
