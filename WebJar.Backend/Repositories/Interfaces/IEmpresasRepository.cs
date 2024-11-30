﻿using WebJar.Shared.Entities;
using WebJar.Shared.Responses;

namespace WebJar.Backend.Repositories.Interfaces
{
    public interface IEmpresasRepository
    {
        Task<ActionResponse<Empresa>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Empresa>>> GetAsync();
    }
}