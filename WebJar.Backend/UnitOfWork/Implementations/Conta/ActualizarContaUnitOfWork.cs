using WebJar.Backend.UnitOfWork.Interfaces.Conta;
using WebJar.Backend.Repositories.Interfaces.Conta;

using WebJar.Frontend.Pages.Conta.Actualizar;

namespace WebJar.Backend.UnitOfWork.Implementations.Conta
{
    public class ActualizarContaUnitOfWork : IActualizarContaUnitOfWork
    {
        private readonly IActualizarContaRepository _actualizarContaRepository;

        public ActualizarContaUnitOfWork(IActualizarContaRepository ActualizarContaRepository)
        {
            _actualizarContaRepository = ActualizarContaRepository;
        }

        public async Task ActualizarConta(int empresaId, int elMes, int elYear) => await _actualizarContaRepository.ActualizarConta(empresaId, elMes, elYear);
    }
}