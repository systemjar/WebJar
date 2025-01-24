using Blazored.Modal.Services;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;

using WebJar.Shared.Servicios;
using WebJar.Frontend.Repositories;
using WebJar.Shared.Entities.Conta;

namespace WebJar.Frontend.Pages.Conta.Actualizar
{
    public partial class ActualizarConta
    {
        [Parameter] public int EmpresaId { get; set; }
        [Inject] private EmpresaService EmpresaService { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        private int Month { get; set; } = 1;
        private int Year { get; set; } = 2025;
        [CascadingParameter] private BlazoredModalInstance BlazoredModal { get; set; } = default!;

        private async Task ActualizarSaldos()
        {
            var url = $"api/actualiza/actualizar?empresaId={EmpresaService.EmpresaSeleccionada.Id}&elMes={Month}&elYear={Year}";

            var responseHttpCM = await Repository.ActualizarSaldosContaAsync(url);
            if (!responseHttpCM)
            {
                //var message = await responseHttpCM.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", "Error en la actualizacion de saldos");
                return;
            }

            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Actualizacion realizada con éxito.");

            NavigationManager.NavigateTo("/");
        }
    }
}