using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using WebJar.Shared.Servicios;

namespace WebJar.Frontend.Layout
{
    public partial class NavMenu
    {
        private bool collapseNavMenu = true;

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        private bool showSubMenuConta = false;
        private bool showSubMenuUsuario = false;
        private bool showSubMenu1 = false;
        private bool showSubSubMenu1 = false;

        private void ToggleSubMenuConta()
        {
            showSubMenuConta = !showSubMenuConta;
        }

        private void ToggleSubSubMenu1()
        {
            showSubSubMenu1 = !showSubSubMenu1;
        }

        private void ToggleSubMenuUsuario()
        {
            showSubMenuUsuario = !showSubMenuUsuario;
        }

        private async Task NavigateToCuentasAsync()
        {
            if (EmpresaService.EmpresaSeleccionada != null && EmpresaService.EmpresaSeleccionada.Id != 0)
            {
                await Task.Delay(100);
                NavigationManager.NavigateTo($"/cuentas/{EmpresaService.EmpresaSeleccionada.Id}");
            }
            else
            {
                await SweetAlertService.FireAsync(new SweetAlertOptions
                {
                    Text = "No ha seleccionado ninguna empresa para trabajar.",
                    Icon = SweetAlertIcon.Question,
                    ShowCancelButton = false
                });
            }
        }

        private async Task NavigateToDocumentosAsync()
        {
            if (EmpresaService.EmpresaSeleccionada != null && EmpresaService.EmpresaSeleccionada.Id != 0)
            {
                await Task.Delay(100);
                NavigationManager.NavigateTo($"/documentos/{EmpresaService.EmpresaSeleccionada.Id}");
            }
            else
            {
                await SweetAlertService.FireAsync(new SweetAlertOptions
                {
                    Text = "No ha seleccionado ninguna empresa para trabajar.",
                    Icon = SweetAlertIcon.Question,
                    ShowCancelButton = false
                });
            }
        }

        private async Task NavigateToActualizarContaAsync()
        {
            if (EmpresaService.EmpresaSeleccionada != null && EmpresaService.EmpresaSeleccionada.Id != 0)
            {
                await Task.Delay(100);
                NavigationManager.NavigateTo($"/actualizarConta");
            }
            else
            {
                await SweetAlertService.FireAsync(new SweetAlertOptions
                {
                    Text = "No ha seleccionado ninguna empresa para trabajar.",
                    Icon = SweetAlertIcon.Question,
                    ShowCancelButton = false
                });
            }
        }
    }
}