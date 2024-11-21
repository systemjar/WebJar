using WebJar.Shared.Entities.Conta;

namespace WebJar.Backend.Data
{
    public class SeedDb(DataContext context)
    {
        private readonly DataContext _context = context;

        public async Task SeedAsync()
        {
            //Metodo que sirve para asegurarse de que haya base de datos, corre un update-database que corre todas las migraciones pendientes
            await _context.Database.EnsureCreatedAsync();
            await CheckTiposConta();
        }

        private async Task CheckTiposConta()
        {
            if (!_context.TiposConta.Any())
            {
                _context.TiposConta.Add(new TipoConta { Nombre = "CHEQUE" });
                _context.TiposConta.Add(new TipoConta { Nombre = "POLIZA" });
            }
            await _context.SaveChangesAsync();
        }
    }
}