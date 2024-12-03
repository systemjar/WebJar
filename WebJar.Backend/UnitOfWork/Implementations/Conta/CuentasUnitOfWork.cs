using WebJar.Backend.Repositories.Interfaces.Conta;
using WebJar.Backend.Repositories.Interfaces.Generico;
using WebJar.Backend.UnitOfWork.Implementations.Gererico;
using WebJar.Backend.UnitOfWork.Interfaces.Conta;
using WebJar.Shared.DTOs;
using WebJar.Shared.Entities;
using WebJar.Shared.Responses;

namespace WebJar.Backend.UnitOfWork.Implementations.Conta
{
    public class CuentasUnitOfWork : GenericUnitOfWork<Cuenta>, ICuentasUnitOfWork
    {
        private readonly IGenericRepository<Cuenta> _cuentasRepository;

        public CuentasUnitOfWork(IGenericRepository<Cuenta> repository, ICuentasRepository cuentasRepository) : base(repository)
        {
            _cuentasRepository = repository;
        }

        public override async Task<ActionResponse<Cuenta>> GetAsync(int id) => await _cuentasRepository.GetAsync(id);

        public override async Task<ActionResponse<IEnumerable<Cuenta>>> GetAsync() => await _cuentasRepository.GetAsync();

        public override async Task<ActionResponse<IEnumerable<Cuenta>>> GetAsync(PaginationDTO pagination) => await _cuentasRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _cuentasRepository.GetTotalPagesAsync(pagination);
    }
}