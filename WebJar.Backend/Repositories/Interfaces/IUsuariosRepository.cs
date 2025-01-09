using Microsoft.AspNetCore.Identity;
using WebJar.Shared.DTOs;
using WebJar.Shared.Entities;

namespace WebJar.Backend.Repositories.Interfaces
{
    public interface IUsuariosRepository
    {
        //Buscar el usuario por email
        Task<Usuario?> GetUserAsync(string email);

        //Buscar usuario por ID cuando confirmamos usuario
        Task<Usuario> GetUserAsync(Guid userId);

        //Adiciona un usuario con su password y regresa si fue existosa la operacion
        Task<IdentityResult> AddUserAsync(Usuario user, string password);

        //Editar usuario
        Task<IdentityResult> UpdateUserAsync(Usuario user);

        //Cambiar contraseña
        Task<IdentityResult> ChangePasswordAsync(Usuario user, string currentPassword, string newPassword);

        //Chequea si existe el rol, si no existe lo crea
        Task CheckRoleAsync(string roleName);

        //Asigna ese rol al usuario
        Task AddUserToRoleAsync(Usuario user, string roleName);

        //Revisa si ese usuario pertenece a ese rol
        Task<bool> IsUserInRoleAsync(Usuario user, string roleName);

        //Para poder hacer Login
        Task<SignInResult> LoginAsync(LoginDTO model);

        //Para Logout
        Task LogoutAsync();

        Task<string> GenerateEmailConfirmationTokenAsync(Usuario user);

        Task<IdentityResult> ConfirmEmailAsync(Usuario user, string token);

        //Para obtener una nueva contraseña cuando se olvida la anterior
        Task<string> GeneratePasswordResetTokenAsync(Usuario user);

        Task<IdentityResult> ResetPasswordAsync(Usuario user, string token, string password);
    }
}