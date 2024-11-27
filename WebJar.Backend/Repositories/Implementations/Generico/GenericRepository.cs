using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using WebJar.Backend.Data;
using WebJar.Backend.Repositories.Interfaces.Generico;
using WebJar.Shared.Responses;

namespace WebJar.Backend.Repositories.Implementations.Generico
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;

        //Inyectamos el DbSet para la entidad
        private readonly DbSet<T> _entity;

        public GenericRepository(DataContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }

        public virtual async Task<ActionResponse<T>> AddAsync(T entity)
        {
            _context.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<T>
                {
                    WasSuccess = true,
                    Result = entity
                };
            }
            catch (DbUpdateException)
            {
                return DbUpdateExceptionActionResponse();
            }
        }

        public virtual async Task<ActionResponse<T>> DeleteAsync(int id)
        {
            var registro = await _entity.FindAsync(id);
            if (registro == null)
            {
                return new ActionResponse<T>
                {
                    WasSuccess = false,
                    Message = "Registro no encontrado"
                };
            }

            try
            {
                _entity.Remove(registro);
                await _context.SaveChangesAsync();
                return new ActionResponse<T>
                {
                    WasSuccess = true,
                };
            }
            catch
            {
                return new ActionResponse<T>
                {
                    WasSuccess = false,
                    Message = "Ese registro no se puede borrar."
                };
            }
        }

        public virtual async Task<ActionResponse<T>> GetAsync(int id)
        {
            var registro = await _entity.FindAsync(id);

            if (registro != null)
            {
                return new ActionResponse<T>
                {
                    WasSuccess = true,
                    Result = registro
                };
            }

            return new ActionResponse<T>
            {
                WasSuccess = false,
                Message = "Registro no encontrado."
            };
        }

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync()
        {
            return new ActionResponse<IEnumerable<T>>
            {
                WasSuccess = true,
                Result = await _entity.ToListAsync()
            };
        }

        public virtual async Task<ActionResponse<T>> UpdateAsync(T entity)
        {
            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return new ActionResponse<T>
                {
                    WasSuccess = true,
                    Result = entity
                };
            }
            catch (DbUpdateException)
            {
                return DbUpdateExceptionActionResponse();
            }
            catch (Exception ex)
            {
                return UpdateExceptionActionResponse(ex);
            }
        }

        private ActionResponse<T> UpdateExceptionActionResponse(Exception ex)
        {
            return new ActionResponse<T>
            {
                WasSuccess = false,
                Message = ex.Message
            };
        }

        private ActionResponse<T> DbUpdateExceptionActionResponse()
        {
            return new ActionResponse<T>
            {
                WasSuccess = false,
                Message = "Ese registro ya existe, favor revisar."
            };
        }
    }
}