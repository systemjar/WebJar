using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using System.Net;
using WebJar.Frontend.Repositories;
using WebJar.Shared.DTOs.Conta;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Servicios;

namespace WebJar.Frontend.Pages.Conta.Documentos
{
    public partial class DocumentosEdit
    {
        //Parametros
        [Parameter] public int PolizaId { get; set; }

        [Parameter] public bool editar { get; set; }

        //DTOs para manejo de datos
        public PolizaDTO Poliza { get; set; } = new PolizaDTO();

        public DetalleDTO LosDetalles { get; set; } = new DetalleDTO();

        //Variable para saber a que empresa pertenece el documento
        public int EmpresaId { get; set; } = 0;

        //Variable para buscar el tipo de documento
        public TipoConta? TipoConta { get; set; }

        //Campos fijos a desplegar
        public string ElDocumento { get; set; } = string.Empty;

        public string ElTipo { get; set; } = string.Empty;
        public string LaFecha { get; set; } = string.Empty;

        //Campos para el ingreso de datos del detalle
        public string ElCodigo { get; set; } = string.Empty;

        public decimal? AlDebe { get; set; } = decimal.Zero;
        public decimal? AlHaber { get; set; } = decimal.Zero;

        //Variables para obtener los datos de la cuenta
        public Cuenta? LaCuenta { get; set; }

        public int LaCuentaId { get; set; }
        public string? LaCuentaNombre { get; set; }

        public int ElTipoId { get; set; }

        private List<DetalleDTO>? DetalleDTOs { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private EmpresaService? EmpresaService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await BuscarPoliza();
        }

        private async Task BuscarPoliza()
        {
            //Obtiene la poliza
            var urlP = $"api/poliza/Id={PolizaId}";
            var responseHttpP = await Repository.GetAsync<PolizaDTO>(urlP);
            Poliza = responseHttpP.Response;
            EmpresaId = Poliza.EmpresaId;

            //Obtiene el nombre del tipo de documento
            var urlT = $"api/tipoconta/Id={Poliza.TipoId}";
            var responseHttpT = await Repository.GetAsync<TipoConta>(urlT);
            TipoConta = responseHttpT.Response;
            ElTipo = TipoConta.Nombre;
        }

        private async Task BuscarCuenta()
        {
            var url = $"api/cuenta/codigo?empresaId={Poliza.EmpresaId}&codigoCuenta={ElCodigo}";

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
            LaCuentaNombre = responseHttp.Response.Nombre;
            LaCuentaId = responseHttp.Response.Id;
            StateHasChanged();
        }

        private async Task EditAsync()
        {
            var polizaNueva = new Poliza
            {
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

                Detalles = Poliza.Detalles.Select(d => new Detalle
                {
                    EmpresaId = EmpresaId,
                    PolizaId = Poliza.Id,
                    TipoId = Poliza.TipoId,
                    // Es posible que necesites ajustar esto según tu lógica
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

        private async Task Return()
        {
            //documentoForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/documentos/{EmpresaId}");
        }

        private async Task AgregarDetalle1()
        {
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

        private async Task EliminarDetalle(int Id)
        {
            var detalle = Poliza.Detalles.FirstOrDefault(d => d.Id == Id);
            if (detalle != null)
            {
                Poliza.Detalles.Remove(detalle);
            }
        }

        private async Task Cancelar()
        {
            Return();
        }
    }
}