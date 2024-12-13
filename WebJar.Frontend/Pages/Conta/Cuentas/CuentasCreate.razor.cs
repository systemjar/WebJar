using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using WebJar.Frontend.Repositories;
using WebJar.Shared.Entities;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Servicios;

namespace WebJar.Frontend.Pages.Conta.Cuentas
{
    [Authorize(Roles = "Admin,Conta")]
    public partial class CuentasCreate
    {
        [Parameter] public int EmpresaId { get; set; }

        private Cuenta cuenta = new();

        private CuentaForm? cuentaForm;

        private string codigoMayor = string.Empty;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private EmpresaService? EmpresaService { get; set; }

        [CascadingParameter] private BlazoredModalInstance BlazoredModal { get; set; } = default!;

        private async Task CreateAsync()
        {
            cuenta.EmpresaId = EmpresaId;

            bool isValid = await ValidarYAsignarCodigoMayor(cuenta, EmpresaService?.EmpresaSeleccionada!);

            if (!isValid)
            {
                return;
            }

            var responseHttp = await Repository.PostAsync("/api/cuenta", cuenta);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message);
                return;
            }

            await BlazoredModal.CloseAsync(ModalResult.Ok());

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

        private async Task<bool> ValidarYAsignarCodigoMayor(Cuenta cuenta, Empresa empresa)
        {
            int[] niveles = new int[]
            {
            int.Parse(empresa.Nivel1),
            int.Parse(empresa.Nivel2),
            int.Parse(empresa.Nivel3),
            int.Parse(empresa.Nivel4),
            int.Parse(empresa.Nivel5),
            int.Parse(empresa.Nivel6)
            };

            var codigo = cuenta.Codigo;

            if (codigo.Length == niveles[0])
            {
                cuenta.CodigoMayor = codigo;
                return true;
            }

            int longitudAcumulada = 0;
            for (int i = 0; i < niveles.Length; i++)
            {
                longitudAcumulada += niveles[i];
                if (codigo.Length == longitudAcumulada)
                {
                    var codigoAnterior = codigo.Substring(0, codigo.Length - niveles[i]);
                    var cuentaAnterior = empresa.Cuentas!.FirstOrDefault(c => c.Codigo == codigoAnterior);
                    if (cuentaAnterior != null)
                    {
                        cuenta.CodigoMayor = codigoAnterior;
                        return true;
                    }
                    else
                    {
                        await SweetAlertService.FireAsync("Error", $"El código de nivel anterior {codigoAnterior} no existe.", SweetAlertIcon.Error);
                        return false;
                        //throw new Exception($"El código de nivel anterior {codigoAnterior} no existe.");
                    }
                }
            }

            await SweetAlertService.FireAsync("Error", "No deberia de haber llegdo aqui Codigo Mayor.", SweetAlertIcon.Error);
            return false;
            //throw new Exception("El Código no tiene una longitud válida según los niveles definidos.");
        }
    }
}