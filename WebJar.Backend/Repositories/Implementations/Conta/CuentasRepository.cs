﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebJar.Backend.Data;
using WebJar.Backend.Helpers;
using WebJar.Backend.Repositories.Implementations.Generico;
using WebJar.Backend.Repositories.Interfaces.Conta;
using WebJar.Shared.DTOs;
using WebJar.Shared.Entities;
using WebJar.Shared.Responses;

namespace WebJar.Backend.Repositories.Implementations.Conta
{
    public class CuentasRepository : GenericRepository<Cuenta>, ICuentasRepository
    {
        private readonly DataContext _context;

        public CuentasRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<Cuenta>> GetAsync(int id)
        {
            var cuenta = await _context.Cuentas
                                .FirstOrDefaultAsync(x => x.Id == id);
            if (cuenta == null)
            {
                return new ActionResponse<Cuenta>
                {
                    WasSuccess = false,
                    Message = "Cuenta no existe"
                };
            }

            return new ActionResponse<Cuenta>
            {
                WasSuccess = true,
                Result = cuenta
            };
        }

        public override async Task<ActionResponse<IEnumerable<Cuenta>>> GetAsync()
        {
            var cuentas = await _context.Cuentas
                                .OrderBy(x => x.Codigo)
                                .ToListAsync();

            return new ActionResponse<IEnumerable<Cuenta>>()
            {
                WasSuccess = true,
                Result = cuentas
            };
        }

        public override async Task<ActionResponse<IEnumerable<Cuenta>>> GetAsync(PaginationDTO pagination)
        {
            var cuentas = _context.Cuentas
                          .Where(x => x.Empresa!.Id == pagination.Id)
                          .AsQueryable();

            return new ActionResponse<IEnumerable<Cuenta>>()
            {
                WasSuccess = true,
                Result = await cuentas
                                .OrderBy(x => x.Codigo)
                                .Paginate(pagination)
                                .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var cuentas = _context.Cuentas
                        .Where(x => x.Empresa!.Id == pagination.Id)
                        .AsQueryable();

            double count = await cuentas.CountAsync();

            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);

            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }
    }
}