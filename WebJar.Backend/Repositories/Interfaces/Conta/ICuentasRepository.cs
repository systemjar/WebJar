using WebJar.Shared.Entities;
using WebJar.Shared.Responses;

namespace WebJar.Backend.Repositories.Interfaces.Conta
{
    public interface ICuentasRepository
    {
        Task<ActionResponse<Cuenta>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Cuenta>>> GetAsync();
    }
}