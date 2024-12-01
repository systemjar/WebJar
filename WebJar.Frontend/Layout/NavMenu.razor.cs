using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;

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
        private bool showSubSubMenu1 = false;
        private bool showSubMenu2 = false;

        private void ToggleSubMenu()
        {
            showSubMenuConta = !showSubMenuConta;
        }

        private void ToggleSubSubMenu1()
        {
            showSubSubMenu1 = !showSubSubMenu1;
        }

        private void ToggleSubMenu2()
        {
            showSubMenu2 = !showSubMenu2;
        }

        public int aa = 2;

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
            //Para el commit
        }
    }
}