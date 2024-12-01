using WebJar.Shared.Entities;
using WebJar.Shared.Responses;

namespace WebJar.Backend.UnitOfWork.Interfaces.Conta
{
    public interface ICuentasUnitOfWork
    {
        Task<ActionResponse<Cuenta>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Cuenta>>> GetAsync();
    }
}