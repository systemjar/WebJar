using WebJar.Shared.Entities;
using WebJar.Shared.Responses;

namespace WebJar.Backend.UnitOfWork.Interfaces
{
    public interface IEmpresasUnitOfWork
    {
        Task<ActionResponse<Empresa>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Empresa>>> GetAsync();
    }
}