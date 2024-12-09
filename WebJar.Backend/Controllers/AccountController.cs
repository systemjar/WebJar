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

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _usuariosUnitOfWork.GetUserAsync(User.Identity!.Name!));
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

        [HttpPost("changePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _usuariosUnitOfWork.GetUserAsync(User.Identity!.Name!);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _usuariosUnitOfWork.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.FirstOrDefault()!.Description);
            }
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutAsync(Usuario user)
        {
            try
            {
                var currentUser = await _usuariosUnitOfWork.GetUserAsync(User.Identity!.Name!);

                if (currentUser == null)
                {
                    return NotFound();
                }

                //if (!string.IsNullOrEmpty(user.Photo))
                //{
                //    var photoUser = Convert.FromBase64String(user.Photo);
                //    user.Photo = await _fileStorage.SaveFileAsync(photoUser, ".jpg", _container);
                //}

                currentUser.Document = user.Document;
                currentUser.FirstName = user.FirstName;
                currentUser.LastName = user.LastName;
                currentUser.Address = user.Address;
                currentUser.PhoneNumber = user.PhoneNumber;
                currentUser.Photo = !string.IsNullOrEmpty(user.Photo) && user.Photo != currentUser.Photo ? user.Photo :
                currentUser.Photo;

                var result = await _usuariosUnitOfWork.UpdateUserAsync(currentUser);

                if (result.Succeeded)
                {
                    return Ok(BuildToken(currentUser));
                }

                return BadRequest(result.Errors.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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