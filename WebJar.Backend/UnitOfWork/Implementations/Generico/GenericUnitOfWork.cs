using WebJar.Backend.Repositories.Interfaces.Generico;
using WebJar.Backend.UnitOfWork.Interfaces.Generico;
using WebJar.Shared.Responses;

namespace WebJar.Backend.UnitOfWork.Implementations.Generico
{
    public class GenericUnitOfWork<T> : IGenericUnitOfWork<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GenericUnitOfWork(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<ActionResponse<T>> AddAsync(T model) => await _repository.AddAsync(model);

        public async Task<ActionResponse<T>> DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async Task<ActionResponse<IEnumerable<T>>> GetAsync() => await _repository.GetAsync();

        public async Task<ActionResponse<T>> GetAsync(int id) => await _repository.GetAsync(id);

        public async Task<ActionResponse<T>> UpdateAsync(T model) => await _repository.UpdateAsync(model);
    }
}