﻿using WebJar.Shared.Responses;

namespace WebJar.Backend.UnitOfWork.Interfaces.Generico
{
    public interface IGenericUnitOfWork<T> where T : class
    {
        Task<ActionResponse<T>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<T>>> GetAsync();

        Task<ActionResponse<T>> AddAsync(T model);

        Task<ActionResponse<T>> UpdateAsync(T model);

        Task<ActionResponse<T>> DeleteAsync(int id);
    }
}