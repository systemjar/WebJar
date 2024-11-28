using WebJar.Backend.UnitOfWork.Interfaces.Generico;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Responses;
using WebJar.Shared.Servicios;

namespace WebJar.Backend.UnitOfWork.Interfaces.Conta
{
    public interface ICuentasUnitOfWork : IGenericUnitOfWork<Cuenta>
    {
        Task<ActionResponse<IEnumerable<Cuenta>>> GetCuentaAsync(string nit);
    }
}