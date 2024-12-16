using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using System.Net;
using WebJar.Frontend.Repositories;
using WebJar.Frontend.Services;
using WebJar.Shared.DTOs;
using WebJar.Shared.Entities;

namespace WebJar.Frontend.Pages.Auth
{
    public partial class EditUser
    {
        private Usuario? user;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ILoginService LoginService { get; set; } = null!;

        [CascadingParameter] private IModalService Modal { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadUserAsyc();
        }

        private async Task LoadUserAsyc()
        {
            var responseHttp = await Repository.GetAsync<Usuario>($"/api/account");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/");
                    return;
                }
                var messageError = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                return;
            }

            user = responseHttp.Response;
        }

        private async Task SaveUserAsync()
        {
            //var responseHttp = await Repository.PutAsync<Usuario>("/api/account", user!);
            var responseHttp = await Repository.PutAsync<Usuario, TokenDTO>("/api/account", user!);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            await LoginService.LoginAsync(responseHttp.Response!.Token);

            NavigationManager.NavigateTo("/");
        }

        private void ShowModal()
        {
            Modal.Show<ChangePassword>();
        }
    }
}