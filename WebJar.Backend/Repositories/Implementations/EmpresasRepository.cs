using Microsoft.EntityFrameworkCore;
using WebJar.Backend.Data;
using WebJar.Backend.Helpers;
using WebJar.Backend.Repositories.Implementations.Generico;
using WebJar.Backend.Repositories.Interfaces;
using WebJar.Shared.DTOs;
using WebJar.Shared.Entities;
using WebJar.Shared.Responses;

namespace WebJar.Backend.Repositories.Implementations
{
    public class EmpresasRepository : GenericRepository<Empresa>, IEmpresasRepository
    {
        private readonly DataContext _context;

        public EmpresasRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<Empresa>> GetAsync(int id)
        {
            var empresa = await _context.Empresas
                                //.Include(x => x.Cuentas!.OrderBy(c => c.Codigo))
                                .FirstOrDefaultAsync(x => x.Id == id);

            if (empresa == null)
            {
                return new ActionResponse<Empresa>
                {
                    WasSuccess = false,
                    Message = "Empresa no existe"
                };
            }
            return new ActionResponse<Empresa>
            {
                WasSuccess = true,
                Result = empresa
            };
        }

        public override async Task<ActionResponse<IEnumerable<Empresa>>> GetAsync()
        {
            var empresas = await _context.Empresas
                                .Include(e => e.Cuentas)
                                .OrderBy(e => e.Nit)
                                .ToListAsync();
            return new ActionResponse<IEnumerable<Empresa>>
            {
                WasSuccess = true,
                Result = empresas
            };
        }

        public override async Task<ActionResponse<IEnumerable<Empresa>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Empresas
            .Include(c => c.Cuentas)
            .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable
                            .Where(x => x.Nombre.ToLower().Contains(pagination.Filter.ToLower()) || x.Nit.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Empresa>>
            {
                WasSuccess = true,
                Result = await queryable
                                .OrderBy(e => e.Nit)
                                .Paginate(pagination)
                                .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Empresas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable
                            .Where(x => x.Nombre.ToLower().Contains(pagination.Filter.ToLower()) || x.Nit.ToLower().Contains(pagination.Filter.ToLower()));
            }

            //Contamos los registros
            double count = await queryable.CountAsync();

            //Redondea haci arriba
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);

            //Regresamos el total de paginas
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }
    }
}