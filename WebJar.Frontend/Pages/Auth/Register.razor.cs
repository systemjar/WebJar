using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using WebJar.Frontend.Repositories;
using WebJar.Frontend.Services;
using WebJar.Shared.DTOs;
using WebJar.Shared.Enums;

namespace WebJar.Frontend.Pages.Auth
{
    public partial class Register
    {
        private UserDTO userDTO = new();
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ILoginService LoginService { get; set; } = null!;

        private bool loading;

        private async Task CreateUserAsync()
        {
            userDTO.UserName = userDTO.Email;
            userDTO.UserType = UserType.Guest;
            loading = true;

            //var responseHttp = await Repository.PostAsync<UserDTO, TokenDTO>("/api/account/CreateUser", userDTO);
            //Ahora usamos el post que no devuelve nada
            var responseHttp = await Repository.PostAsync<UserDTO>("/api/account/CreateUser", userDTO);

            loading = false;

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            await SweetAlertService.FireAsync("Confirmación", "Su cuenta ha sido creada con éxito. Se te ha enviado un correo electrónico con las instrucciones para activar tu usuario.", SweetAlertIcon.Info);

            NavigationManager.NavigateTo(" / ");
        }
    }
}