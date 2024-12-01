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

        //
        public int queEmpresa = 2;

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
    }
}