using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;
using WebJar.Frontend.Repositories;
using WebJar.Shared.DTOs.Conta;
using WebJar.Shared.Entities;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Responses;
using WebJar.Shared.Servicios;

namespace WebJar.Frontend.Pages.Conta.Documentos
{
    public partial class DocumentosEdit
    {
        //Parametros
        [Parameter] public int PolizaId { get; set; }

        [Parameter] public bool editar { get; set; }

        [Inject] private EmpresaService EmpresaService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        private ElementReference debeInput, ElCodigoInput;

        //DTOs para el ingreso de datos
        public Poliza LaPoliza { get; set; }

        public Detalle ElDetalle { get; set; } = new Detalle();

        private DocumentoForm? documentoForm;

        //Lista con los tipos de movimientos
        public List<TipoConta>? TiposConta { get; set; }

        //Variables para el ingreso del detalle
        public string ElCodigo { get; set; } = string.Empty;

        public decimal? AlDebe { get; set; } = decimal.Zero;
        public decimal? AlHaber { get; set; } = decimal.Zero;

        //Variables para guardar la informacion de la cuenta
        public Cuenta? LaCuenta { get; set; }

        public List<CuentaListaDTO> LasCuentas { get; set; } = new List<CuentaListaDTO>();

        // Propiedad para almacenar las cuentas filtradas para el autocompletar
        public List<Cuenta> CuentasFiltradas { get; set; } = new List<Cuenta>();

        public int LaCuentaId { get; set; } = int.MaxValue;
        public string? LaCuentaNombre { get; set; } = string.Empty;

        //Lista para el manejo de los datos del detalle del documento
        private List<Detalle>? LosDetalles { get; set; } = new List<Detalle>();

        protected override async Task OnInitializedAsync()
        {
            var responseHttp = await Repository.GetAsync<Poliza>($"/api/poliza/{PolizaId}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    Return();
                }
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            LaPoliza = responseHttp.Response;
            LaPoliza.Detalles = LaPoliza.Detalles
                .OrderBy(d => d.Codigo)
                .ToList();

            ListadeCuentas();
        }

        private async void ListadeCuentas()
        {
            //Trae una lista de cuentas de detalle para el autocompletar desde el formulario
            var url = $"api/cuenta/buscar?empresaId={LaPoliza.EmpresaId}&autoCompletar=true";

            var responseHttpC = await Repository.GetAsync<List<CuentaListaDTO>>(url);
            if (responseHttpC.Error)
            {
                var message = await responseHttpC.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            LasCuentas = responseHttpC.Response;
        }

        private async Task BuscarCuenta()
        {
            //Busca el codigo de cuenta para validar que existe
            var url = $"api/cuenta/codigo?empresaId={LaPoliza.EmpresaId}&codigoCuenta={ElCodigo}";

            var responseHttp = await Repository.GetAsync<Cuenta>(url);
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    await SweetAlertService.FireAsync("Error", "Ese Codigo de cuenta no existe", SweetAlertIcon.Error);
                    return;
                }
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            LaCuenta = responseHttp.Response;
            if (LaCuenta.EsCuentaDetalle == false)
            {
                await SweetAlertService.FireAsync("Error", "No se le puede hacer movimientos a una cuenta mayor", SweetAlertIcon.Error);
                return;
            }
            LaCuentaNombre = responseHttp.Response.Nombre;
            LaCuentaId = responseHttp.Response.Id;
            StateHasChanged();
            await debeInput.FocusAsync();
        }

        private async Task EditAsync()
        {
            var responseHttp = await Repository.PutAsync("/api/poliza/full", LaPoliza);

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message);
                return;
            }
            Return();
        }

        private void Return()
        {
            //documentoForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/documentos/{LaPoliza.EmpresaId}");
        }

        private async void AgregarDetalle()
        {
            //Agrega los datos el DetalleDTO
            ElDetalle.Codigo = ElCodigo.ToString();
            ElDetalle.Debe = (decimal)AlDebe;
            ElDetalle.Haber = (decimal)AlHaber;
            ElDetalle.CuentaId = LaCuentaId;
            ElDetalle.Cuenta = LaCuenta;
            ElDetalle.Serie = string.Empty;
            ElDetalle.Origen = "Conta";
            ElDetalle.Contras = string.Empty;
            ElDetalle.Factura = string.Empty;
            ElDetalle.TipoId = LaPoliza.TipoId;
            if (LaPoliza.Detalles == null)
            {
                LaPoliza.Detalles = new List<Detalle>();
            }

            // Agrega el nuevo detalle a la colección
            LaPoliza.Detalles.Add(ElDetalle);
            // Reinicia los detalles
            ElDetalle = new Detalle();
            LaCuentaNombre = string.Empty;
            ElCodigo = string.Empty;
            AlDebe = 0;
            AlHaber = 0;
            StateHasChanged();
            await ElCodigoInput.FocusAsync();
        }

        private void EliminarDetalle(int Id)
        {
            var detalle = LaPoliza.Detalles.FirstOrDefault(d => d.Id == Id);
            if (detalle != null)
            {
                LaPoliza.Detalles.Remove(detalle);
            }
        }

        private void Cancelar()
        {
            Return();
        }
    }
}