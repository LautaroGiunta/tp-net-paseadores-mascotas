using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;


namespace Data
{
    public class PaseadoresContext : DbContext
    {
        public PaseadoresContext(DbContextOptions<PaseadoresContext> options) : base(options)
        {
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
                            "Admin123", RolUsuario.Admin, fechaAlta));

            modelBuilder.Entity<Dueno>().HasData(
                new Dueno(2, "Ana", "Gomez", "ana.gomez@paseos.com", "3415550101",
                          "Sarmiento 1234", "Dueno123", fechaAlta));

            modelBuilder.Entity<Paseador>().HasData(
                new Paseador(3, "Carlos", "Ruiz", "carlos.ruiz@paseos.com", "3415550102",
                             "Centro", 3500m, "Paseo123", fechaAlta));
        }
    }
}
