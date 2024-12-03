using WebJar.Backend.Repositories.Interfaces;
using WebJar.Backend.Repositories.Interfaces.Generico;
using WebJar.Backend.UnitOfWork.Implementations.Gererico;
using WebJar.Backend.UnitOfWork.Interfaces;
using WebJar.Shared.DTOs;
using WebJar.Shared.Entities;
using WebJar.Shared.Responses;

namespace WebJar.Backend.UnitOfWork.Implementations
{
    public class EmpresasUnitOfWork : GenericUnitOfWork<Empresa>, IEmpresasUnitOfWork
    {
        private readonly IEmpresasRepository _empresasRepository;

        public EmpresasUnitOfWork(IGenericRepository<Empresa> repository, IEmpresasRepository empresasRepository) : base(repository)
        {
            _empresasRepository = empresasRepository;
        }

        public override async Task<ActionResponse<Empresa>> GetAsync(int id) => await _empresasRepository.GetAsync(id);

        public override async Task<ActionResponse<IEnumerable<Empresa>>> GetAsync() => await _empresasRepository.GetAsync();

        public override async Task<ActionResponse<IEnumerable<Empresa>>> GetAsync(PaginationDTO pagination) => await _empresasRepository.GetAsync(pagination);
    }
}