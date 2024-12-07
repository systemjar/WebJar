﻿using Microsoft.AspNetCore.Identity;
using WebJar.Backend.Repositories.Interfaces;
using WebJar.Backend.UnitOfWork.Interfaces;
using WebJar.Shared.Entities;

namespace WebJar.Backend.UnitOfWork.Implementations
{
    public class UsuariosUnitOfWork : IUsuariosUnitOfWork
    {
        private readonly IUsuariosRepository _usersRepository;

        public UsuariosUnitOfWork(IUsuariosRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<IdentityResult> AddUserAsync(Usuario user, string password) => await
        _usersRepository.AddUserAsync(user, password);

        public async Task AddUserToRoleAsync(Usuario user, string roleName) => await
        _usersRepository.AddUserToRoleAsync(user, roleName);

        public async Task CheckRoleAsync(string roleName) => await _usersRepository.CheckRoleAsync(roleName);

        public async Task<Usuario> GetUserAsync(string email) => await _usersRepository.GetUserAsync(email);

        public async Task<bool> IsUserInRoleAsync(Usuario user, string roleName) => await
        _usersRepository.IsUserInRoleAsync(user, roleName);
    }
}