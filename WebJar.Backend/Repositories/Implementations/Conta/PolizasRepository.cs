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
                                .Include(p => p.Empresa)
                                .Include(p => p.Tipo)
                                .Include(p => p.Detalles)
                                    .ThenInclude(d => d.Cuenta)
                                .FirstOrDefaultAsync(x => x.Id == id);

            //.Include(p => p.Empresa)
            //.Include(p => p.Detalles)
            //    .ThenInclude(d => d.Cuenta)
            //.Include(p => p.Detalles)
            //    .ThenInclude(d => d.Tipo)

            //var poliza = await _context.Polizas
            //                    .FirstOrDefaultAsync(x => x.Id == id);
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
                polizas = polizas
                    .Where(x => x.Documento.ToLower().Contains(pagination.Filter.ToLower())
                    || x.Comentario.ToLower().Contains(pagination.Filter.ToLower())
                    || x.Aquien.ToLower().Contains(pagination.Filter.ToLower())
                    || x.Porque.ToLower().Contains(pagination.Filter.ToLower()));
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

        public async Task<ActionResponse<Poliza>> UpdateFullAsync(Poliza Poliza)
        {
            _context.Entry(Poliza).State = EntityState.Modified;

            foreach (var detalle in Poliza.Detalles)
            {
                if (detalle.Id != 0)
                {
                    _context.Entry(detalle).State = EntityState.Modified;
                }
                else
                {
                    _context.Entry(detalle).State = EntityState.Added;
                }
            }

            var listadoDetallesIds = Poliza.Detalles.Select(x => x.Id).ToList();
            var detallesABorrar = await _context.Detalles
                                        .Where(x => !listadoDetallesIds.Contains(x.Id) && x.PolizaId == Poliza.Id)
                                        .ToListAsync();

            _context.Detalles.RemoveRange(detallesABorrar);

            //_context.Update(Poliza);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Manejo de errores específicos de la base de datos
                Console.WriteLine("Error al actualizar la base de datos: " + ex.Message);
            }
            catch (Exception ex)
            { // Manejo de otros tipos de errores
                Console.WriteLine("Error general: " + ex.Message);
            }
            return new ActionResponse<Poliza>
            {
                WasSuccess = true,
                Result = Poliza
            };
        }

        public override async Task<ActionResponse<Poliza>> DeleteAsync(int id)
        {
            var poliza = await _context.Polizas
            .Include(x => x.Detalles)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (poliza == null)
            {
                return new ActionResponse<Poliza>
                {
                    WasSuccess = false,
                    Message = "Documento no encontrado"
                };
            }

            try
            {
                //Borramos de las tablas Detalles para  luego borrar el producto Products
                _context.Detalles.RemoveRange(poliza.Detalles!);
                _context.Polizas.Remove(poliza);
                await _context.SaveChangesAsync();
                return new ActionResponse<Poliza>
                {
                    WasSuccess = true,
                };
            }
            catch
            {
                return new ActionResponse<Poliza>
                {
                    WasSuccess = false,
                    Message = "No se puede borrar el Documento, porque tiene registros relacionados"
                };
            }
        }
    }
}