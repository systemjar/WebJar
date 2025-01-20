using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using System.Net;
using WebJar.Frontend.Repositories;
using WebJar.Shared.DTOs.Conta;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Servicios;

namespace WebJar.Frontend.Pages.Conta.Documentos
{
    public partial class DocumentosCreate
    {
        //Id de la empresa a la que pertnece
        [Parameter] public int EmpresaId { get; set; }

        private ElementReference debeInput;

        //DTOs para el ingreso de datos
        public PolizaDTO Poliza { get; set; } = new PolizaDTO();

        public DetalleDTO LosDetalles { get; set; } = new DetalleDTO();

        private DocumentoForm? documentoForm;

        //Lista con los tipos de movimientos
        public List<TipoConta>? TiposConta { get; set; }

        //Variables para el ingreso del detalle
        public string ElCodigo { get; set; } = string.Empty;

        public decimal? AlDebe { get; set; } = decimal.Zero;
        public decimal? AlHaber { get; set; } = decimal.Zero;

        //Variables para guardar la informacion de la cuenta
        public Cuenta? LaCuenta { get; set; }

        public List<Cuenta> LasCuentas { get; set; } = new List<Cuenta>();

        // Propiedad para almacenar las cuentas filtradas para el autocompletar
        public List<Cuenta> CuentasFiltradas { get; set; } = new List<Cuenta>();

        public int LaCuentaId { get; set; } = int.MaxValue;
        public string? LaCuentaNombre { get; set; } = string.Empty;

        //Variable para ver el Id del tipo de documento
        public int ElTipoId { get; set; }

        //Lista para el manejo de los datos del detalle del documento
        private List<DetalleDTO>? detalleDTOs { get; set; }

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private EmpresaService? EmpresaService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //Trae en combo con los tipos de documentos para seleccionar desde el formulario
            var responseHttp = await Repository.GetAsync<List<TipoConta>>("/api/tipoconta/combo");
            TiposConta = responseHttp.Response;
            Poliza.Fecha = DateTime.UtcNow;

            //Trae una lista de cuentas de detalle para el autocompletar desde el formulario
            var url = $"api/cuenta/buscar?empresaId={EmpresaId}&autoCompletar=true";

            var responseHttpC = await Repository.GetAsync<List<Cuenta>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            LasCuentas = responseHttpC.Response;
        }

        private void TipoChanged(ChangeEventArgs e)
        {
            //Cuando selecciona algun tipo de documento lo asinga al PolizaDTO
            var selectedTipoId = Convert.ToInt32(e.Value!);
            Poliza.TipoId = selectedTipoId;
            var selectedTipo = TiposConta?.FirstOrDefault(t => t.Id == selectedTipoId);
            ElTipoId = selectedTipoId;
            //VerificarDocumentoAsync();
        }

        private async Task BuscarCuenta()
        {
            //Busca el codigo de cuenta para validar que existe
            var url = $"api/cuenta/codigo?empresaId={EmpresaId}&codigoCuenta={ElCodigo}";

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

        private async Task CreateAsync()
        {
            //Crea los nuevos registros tanto de la Poliza como del Detalle
            var polizaNueva = new Poliza
            {
                //Rellena los campos de la nueva entidad Poliza
                Documento = Poliza.Documento,
                TipoId = Poliza.TipoId,
                Fecha = Poliza.Fecha,
                ElMes = Poliza.Fecha.ToString("MM/yyyy"),
                Aquien = Poliza.Aquien,
                Porque = Poliza.Porque,
                Comentario = Poliza.Comentario,
                EmpresaId = EmpresaId,
                Origen = "Conta",
                FechaOperado = DateTime.UtcNow,

                //TODO: revisar que haya detalle
                Detalles = Poliza.Detalles.Select(d => new Detalle
                {
                    //Rellena los datos de la entidad Detalle
                    //EmpresaId = EmpresaId,
                    PolizaId = Poliza.Id,
                    TipoId = Poliza.TipoId,
                    CuentaId = d.CuentaId,
                    Codigo = d.Codigo,
                    Debe = d.Debe,
                    Haber = d.Haber,
                    Origen = "Conta",
                    Serie = string.Empty,
                    Contras = string.Empty,
                    Factura = string.Empty
                }).ToList()
            };
            try
            {
                var responseHttp = await Repository.PostAsync("/api/poliza", polizaNueva);
                if (responseHttp.Error)
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", message);
                    return;
                }
                Return();
            }
            catch (Exception ex)
            {
                await SweetAlertService.FireAsync("Error", "Hubo un problema al crear la póliza: " + ex.Message, SweetAlertIcon.Error);
            }
        }

        private void Return()
        {
            //documentoForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/documentos/{EmpresaId}");
        }

        private void AgregarDetalle()
        {
            //Agrega los datos el DetalleDTO
            LosDetalles.Codigo = ElCodigo.ToString();
            LosDetalles.Debe = (decimal)AlDebe;
            LosDetalles.Haber = (decimal)AlHaber;
            LosDetalles.CuentaId = LaCuentaId;
            LosDetalles.Cuenta = LaCuenta;
            if (Poliza.Detalles == null)
            {
                Poliza.Detalles = new List<DetalleDTO>();
            }

            // Agrega el nuevo detalle a la colección
            Poliza.Detalles.Add(LosDetalles);
            // Reinicia los detalles
            LosDetalles = new DetalleDTO();
            LaCuentaNombre = string.Empty;
            ElCodigo = string.Empty;
            AlDebe = 0;
            AlHaber = 0;
            StateHasChanged();
        }

        private void EliminarDetalle(int Id)
        {
            var detalle = Poliza.Detalles.FirstOrDefault(d => d.Id == Id);
            if (detalle != null)
            {
                Poliza.Detalles.Remove(detalle);
            }
        }

        private void Cancelar()
        {
            Return();
        }
    }
}