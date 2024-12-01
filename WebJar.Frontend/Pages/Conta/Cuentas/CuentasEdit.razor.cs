using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using System.Net;
using WebJar.Frontend.Pages.Empresas;
using WebJar.Frontend.Repositories;
using WebJar.Shared.Entities;

namespace WebJar.Frontend.Pages.Conta.Cuentas
{
    public partial class CuentasEdit
    {
        [Parameter] public int CuentaId { get; set; }

        private Cuenta? cuenta;

        private CuentaForm? cuentaForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        protected override async Task OnParametersSetAsync()
        {
            var responseHttp = await Repository.GetAsync<Cuenta>($"/api/cuenta/{CuentaId}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    Return();
                }
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            cuenta = responseHttp.Response;
        }

        private async Task EditAsync()
        {
            var responseHttp = await Repository.PutAsync("/api/cuenta", cuenta);
            if (responseHttp.Error)
            {
                var mensajeError = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                return;
            }
            Return();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios guardados con éxito.");
        }

        private void Return()
        {
            cuentaForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/cuentas/{cuenta.EmpresaId}");
        }
    }
}