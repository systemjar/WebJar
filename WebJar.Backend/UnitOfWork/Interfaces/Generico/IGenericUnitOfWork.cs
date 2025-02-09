﻿using WebJar.Shared.DTOs;
using WebJar.Shared.Responses;

namespace WebJar.Backend.UnitOfWork.Interfaces.Generico
{
    public interface IGenericUnitOfWork<T> where T : class
    {
        Task<ActionResponse<T>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<T>>> GetAsync();

        //Para obtener una lista con paginacion
        Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination);

        //Para obtener una lista con paginacion necesito saber cuantas pagins tiene la lista
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<ActionResponse<T>> AddAsync(T entity);

        Task<ActionResponse<T>> DeleteAsync(int id);

        Task<ActionResponse<T>> UpdateAsync(T entity);
    }
}