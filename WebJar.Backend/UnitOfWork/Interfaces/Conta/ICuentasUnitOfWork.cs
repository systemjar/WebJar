﻿using WebJar.Shared.DTOs;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Responses;

namespace WebJar.Backend.UnitOfWork.Interfaces.Conta
{
    public interface ICuentasUnitOfWork
    {
        Task<ActionResponse<Cuenta>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Cuenta>>> GetAsync();

        Task<ActionResponse<IEnumerable<Cuenta>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}