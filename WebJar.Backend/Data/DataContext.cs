using Microsoft.EntityFrameworkCore;
using WebJar.Shared.Entities;
using WebJar.Shared.Entities.Conta;

namespace WebJar.Backend.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<DefPoliza> DefPolizas { get; set; }

        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<Cuenta> Cuentas { get; set; }

        public DbSet<TipoConta> TiposConta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Para crear el indice de la table TipoConta por Nombre unico
            modelBuilder.Entity<TipoConta>().HasIndex(x => x.Nombre).IsUnique();
            modelBuilder.Entity<Cuenta>().HasIndex(x => new { x.Nit, x.Codigo }).IsUnique();
            //modelBuilder.Entity<DefPoliza>().HasIndex(x => new { x.EmpresaId, x.Codigo }).IsUnique();
            //modelBuilder.Entity<Empresa>().HasIndex(x => x.Nit).IsUnique();

            //Desabilitamos el metodo de borrado en cascada
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