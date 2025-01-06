using WebJar.Shared.DTOs;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Responses;

namespace WebJar.Backend.Repositories.Interfaces.Conta
{
    public interface IPolizasRepository
    {
        Task<ActionResponse<Poliza>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Poliza>>> GetAsync();

        Task<ActionResponse<IEnumerable<Poliza>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<ActionResponse<Poliza>> UpdateFullAsync(int id);
    }
}