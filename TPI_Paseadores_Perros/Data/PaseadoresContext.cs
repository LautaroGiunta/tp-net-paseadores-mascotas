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

            // Como las entidades no tienen propiedades de navegacion, las claves foraneas
            // se declaran a mano. Restrict evita que borrar un dueno arrastre a sus perros.
            modelBuilder.Entity<Perro>()
                .HasOne<Dueno>()
                .WithMany()
                .HasForeignKey(p => p.DuenoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Paseo>()
                .HasOne<Paseador>()
                .WithMany()
                .HasForeignKey(p => p.PaseadorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Paseo>()
                .HasOne<Perro>()
                .WithMany()
                .HasForeignKey(p => p.PerroId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
