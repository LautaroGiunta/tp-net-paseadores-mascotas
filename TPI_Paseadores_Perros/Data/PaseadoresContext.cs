using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Data
{
    public class PaseadoresContext : DbContext
    {
        internal const string HashAdmin123 = "$2a$11$awijDt6KCL2HjFegQYT9veuBOQs5e73wuIPuemlA93B.t9LVoQA3y";
        internal const string HashDueno123 = "$2a$11$myJNnok7ay2koW4KqjG4E..7zSv7OpNgX0EyC30h2.pi1KbnzlU1.";
        internal const string HashPaseo123 = "$2a$11$DoRb5PcZZGgn/IP3.TgII.6kK9/FGHsfnvQlCYElPSCmJcWfOOsoG";
        private const string IndiceEmailUnico = "IX_Usuarios_Email";

        public PaseadoresContext(DbContextOptions<PaseadoresContext> options) : base(options)
        {
        }

        // Los servicios controlan el email antes de guardar, pero si dos altas llegan juntas
        // las dos pueden pasar ese control. El índice frena la segunda y acá se traduce a
        // ArgumentException, para que la API responda 400 como con las demás validaciones.
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sql
                                               && (sql.Number == 2601 || sql.Number == 2627)
                                               && sql.Message.Contains(IndiceEmailUnico))
            {
                throw new ArgumentException("Ya existe un usuario con ese email.", ex);
            }
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Paseador> Paseadores { get; set; }
        public DbSet<Dueno> Duenos { get; set; }
        public DbSet<Perro> Perros { get; set; }
        public DbSet<Paseo> Paseos { get; set; }
        public DbSet<Liquidacion> Liquidaciones { get; set; }
        public DbSet<LiquidacionDetalle> LiquidacionDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Dueno>().ToTable("Duenos");

            // El email identifica al usuario en el login: único entre administradores,
            // dueños y paseadores. Con nvarchar(max) SQL Server no permite el índice.
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Email)
                .HasMaxLength(Usuario.LargoMaximoEmail);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName(IndiceEmailUnico);


            modelBuilder.Entity<Paseador>()
                .ToTable("Paseadores")
                .Property(p => p.TarifaPorHora)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Paseo>()
                .Property(p => p.PrecioTotal)
                .HasPrecision(10, 2);

            // Sin propiedades de navegación, las claves foráneas se declaran a mano.
            // Restrict evita que borrar un dueño arrastre a sus perros
            modelBuilder.Entity<Perro>()
                .HasOne<Dueno>(p => p.Dueno)
                .WithMany()
                .HasForeignKey(p => p.DuenoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Paseo>()
                .HasOne<Paseador>(p => p.Paseador)
                .WithMany()
                .HasForeignKey(p => p.PaseadorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Paseo>()
                .HasOne<Perro>(p => p.Perro)
                .WithMany()
                .HasForeignKey(p => p.PerroId)
                .OnDelete(DeleteBehavior.Restrict);

            // Maestro/Detalle: las líneas se borran con su liquidación (Cascade),
            // pero un paseo ya liquidado no se puede borrar (Restrict)
            modelBuilder.Entity<Liquidacion>()
                .HasOne<Paseador>(l => l.Paseador)
                .WithMany()
                .HasForeignKey(l => l.PaseadorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Liquidacion>()
                .HasMany(l => l.Detalles)
                .WithOne()
                .HasForeignKey(d => d.LiquidacionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LiquidacionDetalle>()
                .Property(d => d.Importe)
                .HasPrecision(10, 2);

            modelBuilder.Entity<LiquidacionDetalle>()
                .HasOne<Paseo>(d => d.Paseo)
                .WithMany()
                .HasForeignKey(d => d.PaseoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Un paseo se paga una sola vez
            modelBuilder.Entity<LiquidacionDetalle>()
                .HasIndex(d => d.PaseoId)
                .IsUnique();

            SeedUsuarios(modelBuilder);
        }

        // Uno por cada rol, si no la tabla nace vacía y no hay con quien entrar
        private static void SeedUsuarios(ModelBuilder modelBuilder)
        {
            // Fija a propósito: con DateTime.Now, EF ve un cambio en cada migración
            var fechaAlta = new DateTime(2026, 1, 1);

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario(1, "Admin", "Sistema", "admin@paseos.com", "3415550100",
                            HashAdmin123, RolUsuario.Admin, fechaAlta));

            modelBuilder.Entity<Dueno>().HasData(
                new Dueno(2, "Ana", "Gomez", "ana.gomez@paseos.com", "3415550101",
                          "Sarmiento 1234", HashDueno123, fechaAlta));

            modelBuilder.Entity<Paseador>().HasData(
                new Paseador(3, "Carlos", "Ruiz", "carlos.ruiz@paseos.com", "3415550102",
                             "Centro", 3500m, HashPaseo123, fechaAlta));
        }
    }
}
