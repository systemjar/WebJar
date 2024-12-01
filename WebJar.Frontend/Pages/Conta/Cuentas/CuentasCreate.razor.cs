using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using WebJar.Frontend.Pages.Empresas;
using WebJar.Frontend.Repositories;
using WebJar.Shared.Entities;

namespace WebJar.Frontend.Pages.Conta.Cuentas
{
    public partial class CuentasCreate
    {
        [Parameter] public int EmpresaId { get; set; }

        private Cuenta cuenta = new();

        private CuentaForm? cuentaForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        private async Task CreateAsync()
        {
            cuenta.EmpresaId = EmpresaId;

            var responseHttp = await Repository.PostAsync("/api/cuenta", cuenta);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message);
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con éxito.");
        }

        private void Return()
        {
            cuentaForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/cuentas/{cuenta.EmpresaId}");
        }
    }
}