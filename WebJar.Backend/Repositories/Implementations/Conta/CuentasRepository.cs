using Microsoft.EntityFrameworkCore;
using WebJar.Backend.Data;
using WebJar.Backend.Repositories.Implementations.Generico;
using WebJar.Backend.Repositories.Interfaces.Conta;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Responses;
using WebJar.Shared.Servicios;

namespace WebJar.Backend.Repositories.Implementations.Conta
{
    public class CuentasRepository : GenericRepository<Cuenta>, ICuentasRepository
    {
        private readonly DataContext _context;

        public CuentasRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ActionResponse<IEnumerable<Cuenta>>> GetCuentaAsync(string nit)
        {
            var cuentas = await _context.Cuentas
                            .Where(x => x.Nit == nit)
                            .OrderBy(x => x.Codigo)
                            .ToListAsync();
            return new ActionResponse<IEnumerable<Cuenta>>
            {
                WasSuccess = true,
                Result = cuentas
            };
        }
    }
}