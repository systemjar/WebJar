using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using WebJar.Frontend.Pages.Conta.TiposConta;
using WebJar.Frontend.Repositories;
using WebJar.Shared.Entities.Conta;

namespace WebJar.Frontend.Pages.Conta.Cuentas
{
    public partial class CuentasEdit
    {
        private Cuenta? cuenta;

        private CuentasForm? cuentasForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Parameter] public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var responseHttp = await Repository.GetAsync<Cuenta>($"api/cuenta/{Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("tiposconta");
                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                }
            }
            else
            {
                cuenta = responseHttp.Response;
            }
        }

        private async Task EditAsync()
        {
            var responseHttp = await Repository.PutAsync("api/cuenta", cuenta);
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
            cuentasForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/cuentas");
        }
    }
}