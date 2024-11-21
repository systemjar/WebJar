using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using WebJar.Frontend.Repositories;
using WebJar.Shared.Entities;

namespace WebJar.Frontend.Pages.Conta.TiposConta
{
    public partial class TiposContaCreate
    {
        private TipoConta tipoConta = new();

        private TiposContaForm? tiposContaForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/tipoconta", tipoConta);
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
            tiposContaForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/tiposconta");
        }
    }
}