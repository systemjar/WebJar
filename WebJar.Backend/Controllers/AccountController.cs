﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebJar.Backend.UnitOfWork.Interfaces;
using WebJar.Shared.DTOs;
using WebJar.Shared.Entities;

namespace WebJar.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUsuariosUnitOfWork _usuariosUnitOfWork;
        private readonly IConfiguration _configuration;

        public AccountController(IUsuariosUnitOfWork usersUnitOfWork, IConfiguration configuration)
        {
            _usuariosUnitOfWork = usersUnitOfWork;
            _configuration = configuration;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO model)
        {
            Usuario user = model;
            var result = await _usuariosUnitOfWork.AddUserAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _usuariosUnitOfWork.AddUserToRoleAsync(user, user.UserType.ToString());
                return Ok(BuildToken(user));
            }
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO model)
        {
            var result = await _usuariosUnitOfWork.LoginAsync(model);
            if (result.Succeeded)
            {
                var user = await _usuariosUnitOfWork.GetUserAsync(model.Email);
                return Ok(BuildToken(user!));
            }
            return BadRequest("Email o contraseña incorrectos.");
        }

        private TokenDTO BuildToken(Usuario user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Email!),
                new(ClaimTypes.Role, user.UserType.ToString()),
                new("Document", user.Document),
                new("FirstName", user.FirstName),
                new("LastName", user.LastName),
                new("Address", user.Address),
                new("Photo", user.Photo ?? string.Empty),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(30);
            var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials);
            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}