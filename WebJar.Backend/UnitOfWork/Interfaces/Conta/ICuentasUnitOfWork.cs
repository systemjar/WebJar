using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Responses;
using WebJar.Shared.Servicios;

namespace WebJar.Backend.UnitOfWork.Interfaces.Conta
{
    public interface ICuentasUnitOfWork
    {
        Task<ActionResponse<Cuenta>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Cuenta>>> GetAsync();
    }
}