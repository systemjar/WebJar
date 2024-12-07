﻿using Microsoft.AspNetCore.Identity;
using WebJar.Shared.DTOs;
using WebJar.Shared.Entities;
using WebJar.Shared.Responses;

namespace WebJar.Backend.Repositories.Interfaces
{
    public interface IUsuariosRepository
    {
        //Buscar el usuario por email
        Task<Usuario> GetUserAsync(string email);

        //Adiciona un usuario con su password y regresa si fue existosa la operacion
        Task<IdentityResult> AddUserAsync(Usuario user, string password);

        //Chequea si existe el rol, si no existe lo crea
        Task CheckRoleAsync(string roleName);

        //Asigna ese rol al usuario
        Task AddUserToRoleAsync(Usuario user, string roleName);

        //Revisa si ese usuario pertenece a ese rol
        Task<bool> IsUserInRoleAsync(Usuario user, string roleName);
    }
}