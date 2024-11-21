using Microsoft.EntityFrameworkCore;
using WebJar.Shared.Entities.Conta;

namespace WebJar.Backend.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<TipoConta> TiposConta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Para crear el indice de la table TipoConta por Nombre unico
            modelBuilder.Entity<TipoConta>().HasIndex(x => x.Nombre).IsUnique();
        }
    }
}