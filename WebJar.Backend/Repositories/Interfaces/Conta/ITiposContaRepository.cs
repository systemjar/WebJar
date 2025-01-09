using WebJar.Shared.DTOs;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Responses;

namespace WebJar.Backend.Repositories.Interfaces.Conta
{
    public interface ITiposContaRepository
    {
        Task<ActionResponse<IEnumerable<TipoConta>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<TipoConta>> GetComboAsync();
    }
}