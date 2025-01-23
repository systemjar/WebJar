using WebJar.Shared.DTOs;
using WebJar.Shared.DTOs.Conta;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Responses;

namespace WebJar.Backend.UnitOfWork.Interfaces.Conta
{
    public interface ICuentasUnitOfWork
    {
        Task<ActionResponse<Cuenta>> GetAsync(int id);

        Task<ActionResponse<Cuenta>> GetAsync(int empresaid, int cuenta);

        Task<ActionResponse<Cuenta>> GetAsync(int empresaid, string codigo);

        Task<ActionResponse<IEnumerable<CuentaListaDTO>>> GetAsync(int empresaId, bool autoCompletar);

        Task<ActionResponse<IEnumerable<Cuenta>>> GetAsync();

        Task<ActionResponse<IEnumerable<Cuenta>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}