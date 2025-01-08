using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebJar.Shared.Entities;
using WebJar.Shared.Entities.Conta;

namespace WebJar.Backend.Data
{
    //Se le quita la herencia del DbContext y se pone la herencia de IdentityDbContext
    public class DataContext : IdentityDbContext<Usuario>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Cuenta>? Cuentas { get; set; }

        public DbSet<Empresa>? Empresas { get; set; }

        public DbSet<TipoConta>? TiposConta { get; set; }

        public DbSet<Poliza>? Polizas { get; set; }

        public DbSet<Detalle>? Detalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cuenta>()
                .HasIndex(x => new { x.EmpresaId, x.Codigo }).IsUnique();

            modelBuilder.Entity<Empresa>()
                .HasIndex(x => x.Nit).IsUnique();

            modelBuilder.Entity<TipoConta>()
                .HasIndex(x => x.Nombre).IsUnique();

            modelBuilder.Entity<Poliza>()
                .HasIndex(x => new { x.EmpresaId, x.Documento, x.TipoId }).IsUnique();

            //Poliza - Empresa
            modelBuilder.Entity<Poliza>()
            .HasOne(p => p.Empresa) // Una Poliza tiene una Empresa
            .WithMany(e => e.Polizas) // Una Empresa tiene muchas Polizas
            .HasForeignKey(p => p.EmpresaId); // Llave foránea es EmpresaId en Poliza

            //Poliza - Detalle
            modelBuilder.Entity<Poliza>()
            .HasMany(p => p.Detalles) // Una Poliza tiene muchos Detalles
            .WithOne(d => d.Poliza) // Un Detalle tiene una Poliza
            .HasForeignKey(d => d.PolizaId); // Llave foránea es PolizaId en Detalle

            //Detalle - Empresa
            modelBuilder.Entity<Detalle>()
            .HasOne(d => d.Empresa) // Un Detalle tiene una Empresa
            .WithMany(e => e.Detalles) // Una Empresa tiene muchos Detalles
            .HasForeignKey(d => d.EmpresaId); // Llave foránea es EmpresaId en Detalle

            //Detalle - TipoConta
            modelBuilder.Entity<Detalle>()
            .HasOne(d => d.Tipo)
            .WithMany(t => t.Detalles)
            .HasForeignKey(d => d.TipoId);

            //modelBuilder.Entity<Detalle>().HasIndex(x => new { x.EmpresaId, x.Documento, x.TipoId }).IsUnique();

            DisableCascadingDelete(modelBuilder);
        }

        //Implementacion del metodo para eliminar el borrado en cascada
        private void DisableCascadingDelete(ModelBuilder modelBuilder)
        {
            var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            foreach (var relationship in relationships)
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}