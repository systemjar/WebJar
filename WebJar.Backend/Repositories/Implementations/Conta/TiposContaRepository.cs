using Microsoft.EntityFrameworkCore;
using WebJar.Backend.Data;
using WebJar.Backend.Helpers;
using WebJar.Backend.Repositories.Implementations.Generico;
using WebJar.Backend.Repositories.Interfaces.Conta;
using WebJar.Shared.DTOs;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Responses;

namespace WebJar.Backend.Repositories.Implementations.Conta
{
    public class TiposContaRepository : GenericRepository<TipoConta>, ITiposContaRepository
    {
        private readonly DataContext _context;

        public TiposContaRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        //Cambiamos el queryable para poder tomar en cuenta el filtro
        public override async Task<ActionResponse<IEnumerable<TipoConta>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.TiposConta
            .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Nombre.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<TipoConta>>
            {
                WasSuccess = true,
                Result = await queryable
                                .OrderBy(x => x.Nombre)
                                .Paginate(pagination)
                                .ToListAsync()
            };
        }

        public async Task<IEnumerable<TipoConta>> GetComboAsync()
        {
            var comboTipoConta = await _context.TiposConta
                       .OrderBy(c => c.Nombre)
                       .ToListAsync();
            return comboTipoConta;
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.TiposConta
                          .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Nombre.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();

            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);

            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }
    }
}