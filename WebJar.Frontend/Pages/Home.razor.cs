using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using WebJar.Frontend.Repositories;
using WebJar.Shared.Entities;
using WebJar.Shared.Servicios;

namespace WebJar.Frontend.Pages
{
    public partial class Home
    {
        //Para funcionamiento de la paginacion
        private int currentPage = 1;

        private int totalPages;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        [Parameter, SupplyParameterFromQuery] public string Page { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public int RecordsNumber { get; set; } = 10;

        public List<Empresa>? LEmpresas { get; set; } = new List<Empresa>();

        private bool mostrarBoton = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync(int page = 1)
        {
            //var responseHttp = await repository.GetAsync<List<Empresa>>("/api/empresa");
            //if (responseHttp.Error)
            //{
            //    var message = await responseHttp.GetErrorMessageAsync();
            //    await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            //    return;
            //}
            //LEmpresas = responseHttp.Response;

            if (!string.IsNullOrWhiteSpace(Page))
            {
                page = Convert.ToInt32(Page);
            }

            //Llamamos el metodo para cargar la lista de registros
            var ok = await LoadListAsync(page);
            if (ok)
            {
                await LoadPagesAsync();
            }
        }

        private async Task<bool> LoadListAsync(int page)
        {
            //Obtenemos una lista de paises utilizando el componente repository generico que creamos
            //Utilizamos el GetAsync al que se le manda la url "api/countries" y el devuelve el responseHttp con todo el contenido de respuesta, en este caso una lista pero solo de la pagina que necesitamos page
            /*var responseHttp = await Repository.GetAsync<List<Country>>($"api/countries?page={page}");*/

            //Modificamos la url para que tome en cuenta el filtro
            ValidateRecordsNumber(RecordsNumber);
            var url = $"api/empresa?page={page}&recordsnumber={RecordsNumber}";

            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var responseHttp = await Repository.GetAsync<List<Empresa>>(url);

            //Revisamos si hay error
            if (responseHttp.Error)
            {
                //Leemos el error
                var message = await responseHttp.GetErrorMessageAsync();
                //Desplegamos el error
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);

                return false;
            }

            //Si no hubo error asignamos la respuesta a la lista LCountries
            LEmpresas = responseHttp.Response;

            return true;
        }

        private void ValidateRecordsNumber(int recordsnumber)
        {
            if (recordsnumber == 0)
            {
                RecordsNumber = 10;
            }
        }

        //Metodo para cargar la lista paginada
        private async Task LoadPagesAsync()
        {
            ValidateRecordsNumber(RecordsNumber);
            var url = $"api/empresa/totalpages?recordsnumber={RecordsNumber}";

            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var responseHttp = await Repository.GetAsync<int>(url);

            //Revisamos si hay error
            if (responseHttp.Error)
            {
                //Leemos el error
                var message = await responseHttp.GetErrorMessageAsync();
                //Desplegamos el error
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            totalPages = responseHttp.Response;
        }

        //Para aplicar el filtro y refrescar la interfaz
        private async Task FilterCallBack(string filter)
        {
            Filter = filter;
            await ApplyFilterAsync();
            StateHasChanged();
        }

        private async Task SelectedPageAsync(int page)
        {
            currentPage = page;
            await LoadAsync(page);
        }

        private async Task ApplyFilterAsync()
        {
            int page = 1;
            await LoadAsync(page);
            await SelectedPageAsync(page);
        }

        private async Task SelectedRecordsNumberAsync(int recordsnumber)
        {
            RecordsNumber = recordsnumber;
            int page = 1;
            await LoadAsync(page);
            await SelectedPageAsync(page);
        }

        public void SelEmpresa(Empresa empresa)
        {
            EmpresaService.SeleccionarEmpresa(empresa);
        }
    }
}