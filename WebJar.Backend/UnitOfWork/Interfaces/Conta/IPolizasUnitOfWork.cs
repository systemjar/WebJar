using WebJar.Shared.DTOs;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Responses;

namespace WebJar.Backend.UnitOfWork.Interfaces.Conta
{
    public interface IPolizasUnitOfWork
    {
        Task<ActionResponse<Poliza>> DeleteAsync(int id);

        Task<ActionResponse<Poliza>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Poliza>>> GetAsync();

        Task<ActionResponse<Poliza>> GetAsync(int empresaId, string documento, int tipoId);

        Task<ActionResponse<IEnumerable<Poliza>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        //Task<ActionResponse<Poliza>> UpdateFullAsync(int id);

        ////No se implementa porque esta en el generico pero necesitamos hacerlo publico
        //Task<ActionResponse<Poliza>> AddAsync(Poliza poliza);
    }
}