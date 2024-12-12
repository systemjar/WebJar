using Microsoft.AspNetCore.Identity;
using WebJar.Backend.Repositories.Interfaces;
using WebJar.Backend.UnitOfWork.Interfaces;
using WebJar.Shared.DTOs;
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

        public async Task<Usuario?> GetUserAsync(string email) => await _usersRepository.GetUserAsync(email);

        public async Task<Usuario> GetUserAsync(Guid userId) => await _usersRepository.GetUserAsync(userId);

        public async Task<IdentityResult> ChangePasswordAsync(Usuario user, string currentPassword, string newPassword) =>
        await _usersRepository.ChangePasswordAsync(user, currentPassword, newPassword);

        public async Task<IdentityResult> UpdateUserAsync(Usuario user) => await _usersRepository.UpdateUserAsync(user);

        public async Task<bool> IsUserInRoleAsync(Usuario user, string roleName) => await
        _usersRepository.IsUserInRoleAsync(user, roleName);

        public async Task<SignInResult> LoginAsync(LoginDTO model) => await _usersRepository.LoginAsync(model);

        public async Task LogoutAsync() => await _usersRepository.LogoutAsync();

        public async Task<string> GenerateEmailConfirmationTokenAsync(Usuario user) => await _usersRepository.GenerateEmailConfirmationTokenAsync(user);

        public async Task<IdentityResult> ConfirmEmailAsync(Usuario user, string token) => await _usersRepository.ConfirmEmailAsync(user, token);

        //Para recuperacion de contraseña
        public async Task<string> GeneratePasswordResetTokenAsync(Usuario user) => await _usersRepository.GeneratePasswordResetTokenAsync(user);

        public async Task<IdentityResult> ResetPasswordAsync(Usuario user, string token, string password) => await _usersRepository.ResetPasswordAsync(user, token, password);
    }
}