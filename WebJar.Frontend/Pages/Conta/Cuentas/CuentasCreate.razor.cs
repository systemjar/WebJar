using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using WebJar.Frontend.Repositories;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Enums;
using WebJar.Shared.Servicios;

namespace WebJar.Frontend.Pages.Conta.Cuentas
{
    public partial class CuentasCreate
    {
        private Cuenta cuenta = new();

        private CuentasForm? cuentasForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        protected override void OnInitialized()
        {
            cuenta.Nit = EmpresaService.EmpresaSeleccionada.Nit;
        }

        private async Task CreateAsync()
        {
            //cuenta.Nit = EmpresaService.EmpresaSeleccionada.Nit;

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
            cuentasForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/cuentas");
        }
    }
}