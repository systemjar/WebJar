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

            modelBuilder.Entity<Cuenta>().HasIndex(x => new { x.EmpresaId, x.Codigo }).IsUnique();

            modelBuilder.Entity<Empresa>().HasIndex(x => x.Nit).IsUnique();

            modelBuilder.Entity<TipoConta>().HasIndex(x => x.Nombre).IsUnique();

            modelBuilder.Entity<Poliza>().HasIndex(x => new { x.EmpresaId, x.Documento, x.TipoId }).IsUnique();

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