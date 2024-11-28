using WebJar.Backend.Repositories.Interfaces.Generico;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Responses;
using WebJar.Shared.Servicios;

namespace WebJar.Backend.Repositories.Interfaces.Conta
{
    public interface ICuentasRepository : IGenericRepository<Cuenta>
    {
        Task<ActionResponse<IEnumerable<Cuenta>>> GetCuentaAsync(string nit);
    }
}