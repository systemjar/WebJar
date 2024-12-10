using WebJar.Shared.DTOs;
using WebJar.Shared.Entities;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Responses;

namespace WebJar.Backend.UnitOfWork.Interfaces.Conta
{
    public interface ITiposContaUnitOfWork
    {
        Task<ActionResponse<IEnumerable<TipoConta>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<TipoConta>> GetComboAsync();
    }
}