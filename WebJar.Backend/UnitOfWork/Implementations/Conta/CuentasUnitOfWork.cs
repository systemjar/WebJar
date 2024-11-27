using WebJar.Backend.Repositories.Interfaces.Conta;
using WebJar.Backend.Repositories.Interfaces.Generico;
using WebJar.Backend.UnitOfWork.Implementations.Generico;
using WebJar.Backend.UnitOfWork.Interfaces.Conta;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Responses;
using WebJar.Shared.Servicios;

namespace WebJar.Backend.UnitOfWork.Implementations.Conta
{
    public class CuentasUnitOfWork : GenericUnitOfWork<Cuenta>, ICuentasUnitOfWork
    {
        private readonly ICuentasRepository _cuentasRepository;

        public CuentasUnitOfWork(IGenericRepository<Cuenta> repository, ICuentasRepository cuentasRepository) : base(repository)
        {
            _cuentasRepository = cuentasRepository;
        }

        public async Task<ActionResponse<Cuenta>> GetAsync(int id) => await _cuentasRepository.GetAsync(id);

        public async Task<ActionResponse<IEnumerable<Cuenta>>> GetAsync() => await _cuentasRepository.GetAsync();
    }
}