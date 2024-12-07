using WebJar.Backend.Repositories.Implementations.Conta;
using WebJar.Backend.Repositories.Interfaces.Conta;
using WebJar.Backend.Repositories.Interfaces.Generico;
using WebJar.Backend.UnitOfWork.Implementations.Gererico;
using WebJar.Backend.UnitOfWork.Interfaces.Conta;
using WebJar.Shared.DTOs;
using WebJar.Shared.Entities;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Responses;

namespace WebJar.Backend.UnitOfWork.Implementations.Conta
{
    public class TiposContaUnitOfWork : GenericUnitOfWork<TipoConta>, ITiposContaUnitOfWork
    {
        private readonly ITiposContaRepository _tiposContaRepository;

        public TiposContaUnitOfWork(IGenericRepository<TipoConta> repository, ITiposContaRepository tiposContaRepository) : base(repository)
        {
            _tiposContaRepository = tiposContaRepository;
        }

        public override async Task<ActionResponse<IEnumerable<TipoConta>>> GetAsync(PaginationDTO pagination) => await _tiposContaRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _tiposContaRepository.GetTotalPagesAsync(pagination);
    }
}