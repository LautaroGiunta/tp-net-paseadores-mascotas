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
        public DbSet<Paseador> Paseadores { get; set; }
        public DbSet<Dueno> Duenos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Paseador>()
                .Property(p => p.TarifaPorHora)
                .HasPrecision(10, 2);
        }
    }
}
