using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Responses;
using WebJar.Shared.Servicios;

namespace WebJar.Backend.Repositories.Interfaces.Conta
{
    public interface ICuentasRepository
    {
        Task<ActionResponse<Cuenta>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Cuenta>>> GetAsync();
    }
}