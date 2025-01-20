using WebJar.Backend.Repositories.Interfaces.Conta;
using WebJar.Backend.Repositories.Interfaces.Generico;
using WebJar.Backend.UnitOfWork.Implementations.Gererico;
using WebJar.Backend.UnitOfWork.Interfaces.Conta;
using WebJar.Shared.DTOs;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Responses;

namespace WebJar.Backend.UnitOfWork.Implementations.Conta
{
    public class PolizasUnitOfWork : GenericUnitOfWork<Poliza>, IPolizasUnitOfWork
    {
        private readonly IPolizasRepository _polizasRepository;

        public PolizasUnitOfWork(IGenericRepository<Poliza> repository, IPolizasRepository polizasRepository) : base(repository)
        {
            _polizasRepository = polizasRepository;
        }

        public override async Task<ActionResponse<Poliza>> DeleteAsync(int id) => await _polizasRepository.DeleteAsync(id);

        public override async Task<ActionResponse<Poliza>> GetAsync(int id) => await _polizasRepository.GetAsync(id);

        public async Task<ActionResponse<Poliza>> GetAsync(int empresaId, string documento, int tipoId) => await _polizasRepository.GetAsync(empresaId, documento, tipoId);

        public override async Task<ActionResponse<IEnumerable<Poliza>>> GetAsync(PaginationDTO pagination) => await _polizasRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _polizasRepository.GetTotalPagesAsync(pagination);

        public async Task<ActionResponse<Poliza>> UpdateFullAsync(Poliza poliza) => await _polizasRepository.UpdateFullAsync(poliza);
    }
}