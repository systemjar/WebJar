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
                .HasIndex(x => new { x.EmpresaId, x.Documento, x.TipoId, x.ElMes }).IsUnique();

            //Usuario - Empresa
            //Un Usuario puede tener muchas Empresas a la vez que una Empresa puede tener muchos Usuarios
            //Utiliza la tabla-union UsuarioEmpresa
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Empresas)
                .WithMany(e => e.Usuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuarioEmpresa",
                    j => j.HasOne<Empresa>().WithMany().HasForeignKey("EmpresaId"),
                    j => j.HasOne<Usuario>().WithMany().HasForeignKey("UsuarioId"));

            //Cuenta - Empresa
            //Una Cuenta puede tener solo una Empresa pero una Empresa puede tener muchas Cuenta
            modelBuilder.Entity<Cuenta>()
                .HasOne(c => c.Empresa)
                .WithMany(e => e.Cuentas)
                .HasForeignKey(c => c.EmpresaId);

            //Poliza - Detalle
            modelBuilder.Entity<Poliza>()
                .HasMany(p => p.Detalles)
                .WithOne(d => d.Poliza)
                .HasForeignKey(d => d.PolizaId);

            //Poliza - Empresa
            modelBuilder.Entity<Poliza>()
                .HasOne(p => p.Empresa) // Una Poliza tiene una Empresa
                .WithMany(e => e.Polizas) // Una Empresa tiene muchas Polizas
                .HasForeignKey(p => p.EmpresaId); // Llave foránea es EmpresaId en Poliza

            //TipoConta - Poliza
            modelBuilder.Entity<Poliza>()
                .HasOne(p => p.Tipo)
                .WithMany(t => t.Polizas)
                .HasForeignKey(p => p.TipoId);

            //Detalle - Cuenta
            modelBuilder.Entity<Detalle>()
                .HasOne(d => d.Cuenta)
                .WithMany(c => c.Detalles)
                .HasForeignKey(d => d.CuentaId);

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