using WebJar.Shared.DTOs;
using WebJar.Shared.DTOs.Conta;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Responses;

namespace WebJar.Backend.Repositories.Interfaces.Conta
{
    public interface IPolizasRepository
    {
        Task<ActionResponse<Poliza>> DeleteAsync(int id);

        Task<ActionResponse<Poliza>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Poliza>>> GetAsync();

        Task<ActionResponse<Poliza>> GetAsync(int empresaId, string documento, int tipoId);

        Task<ActionResponse<IEnumerable<Poliza>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<ActionResponse<Poliza>> UpdateFullAsync(Poliza poliza);
    }
}