//using CurrieTechnologies.Razor.SweetAlert2;
//using Microsoft.AspNetCore.Components;
//using WebJar.Frontend.Repositories;
//using WebJar.Shared.Entities;
//using WebJar.Shared.Servicios;

//namespace WebJar.Frontend.Pages
//{
//    public partial class Home
//    {
//        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
//        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

//        [Inject] private IRepository Repository { get; set; } = null!;

//        public List<Empresa>? LEmpresas { get; set; }

//        public bool mostrarBoton = false;

//        protected override async Task OnInitializedAsync()
//        {
//            await LoadAsync();
//        }

//        private async Task LoadAsync()
//        {
//            var responseHttp = await Repository.GetAsync<List<Empresa>>("/api/empresa");
//            if (responseHttp.Error)
//            {
//                var message = await responseHttp.GetErrorMessageAsync();
//                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
//                return;
//            }
//            LEmpresas = responseHttp.Response!;
//        }

//        public void SelEmpresa(Empresa empresa)
//        {
//            EmpresaService.SeleccionarEmpresa(empresa);
//        }
//    }
//}