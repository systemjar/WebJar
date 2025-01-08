using Microsoft.EntityFrameworkCore;
using WebJar.Backend.Data;
using WebJar.Backend.Helpers;
using WebJar.Backend.Repositories.Implementations.Generico;
using WebJar.Backend.Repositories.Interfaces.Conta;
using WebJar.Frontend.Pages.Conta.Documentos;
using WebJar.Shared.DTOs;
using WebJar.Shared.DTOs.Conta;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Enums;
using WebJar.Shared.Responses;

namespace WebJar.Backend.Repositories.Implementations.Conta
{
    public class PolizasRepository : GenericRepository<Poliza>, IPolizasRepository
    {
        private readonly DataContext _context;

        public PolizasRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<Poliza>> GetAsync(int id)
        {
            var poliza = await _context.Polizas
                                .FirstOrDefaultAsync(x => x.Id == id);
            if (poliza == null)
            {
                return new ActionResponse<Poliza>
                {
                    WasSuccess = false,
                    Message = "Documento no existe"
                };
            }

            return new ActionResponse<Poliza>
            {
                WasSuccess = true,
                Result = poliza
            };
        }

        public async Task<ActionResponse<Poliza>> GetAsync(int empresaId, string documento, int tipoId)
        {
            var poliza = await _context.Polizas
                                .Include(x => x.Tipo)
                                .Include(x => x.Detalles)
                                .FirstOrDefaultAsync(x => x.EmpresaId == empresaId && x.Documento == documento && x.TipoId == tipoId);
            if (poliza != null)
            {
                return new ActionResponse<Poliza>
                {
                    WasSuccess = false,
                    Message = "Documento ya existe"
                };
            }

            return new ActionResponse<Poliza>
            {
                WasSuccess = true,
                Result = poliza
            };
        }

        public override async Task<ActionResponse<IEnumerable<Poliza>>> GetAsync()
        {
            var polizas = await _context.Polizas
                                .Include(x => x.Tipo)
                                .Include(x => x.Detalles)
                                .OrderBy(x => x.Fecha)
                                .ThenBy(x => x.Documento)
                                .ToListAsync();

            return new ActionResponse<IEnumerable<Poliza>>()
            {
                WasSuccess = true,
                Result = polizas
            };
        }

        public override async Task<ActionResponse<IEnumerable<Poliza>>> GetAsync(PaginationDTO pagination)
        {
            var polizas = _context.Polizas
                          .Include(x => x.Tipo)
                          .Include(x => x.Detalles)
                          .Where(x => x.Empresa!.Id == pagination.Id)
                          .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                polizas = polizas.Where(x => x.Documento.ToLower().Contains(pagination.Filter.ToLower()) ||
                                             x.Comentario.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Poliza>>()
            {
                WasSuccess = true,
                Result = await polizas
                                .OrderBy(x => x.Fecha)
                                .ThenBy(x => x.Documento)
                                .ThenBy(x => x.Tipo)
                                .Paginate(pagination)
                                .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var polizas = _context.Polizas
                        .Where(x => x.Empresa!.Id == pagination.Id)
                        .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                polizas = polizas.Where(x => x.Documento.ToLower().Contains(pagination.Filter.ToLower()) ||
                                             x.Comentario.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await polizas.CountAsync();

            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);

            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

        public async Task<ActionResponse<Poliza>> UpdateFullAsync(PolizaDTO polizaDTO)
        {
            var poliza = await _context.Polizas
            //.Include(s => s.Detalles)
            .FirstOrDefaultAsync(s => s.Id == polizaDTO.Id);

            _context.Update(poliza);
            await _context.SaveChangesAsync();
            return new ActionResponse<Poliza>
            {
                WasSuccess = true,
                Result = poliza
            };
        }
    }
}