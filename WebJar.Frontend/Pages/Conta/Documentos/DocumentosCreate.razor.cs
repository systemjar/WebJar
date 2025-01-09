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
        [Parameter] public int EmpresaId { get; set; }

        public PolizaDTO poliza { get; set; } = new PolizaDTO();

        public DetalleDTO losDetalles { get; set; } = new DetalleDTO();

        private DocumentoForm? documentoForm;
        public List<TipoConta>? tiposConta { get; set; }

        public string elCodigo { get; set; } = string.Empty;
        public decimal? alDebe { get; set; } = decimal.Zero;
        public decimal? alHaber { get; set; } = decimal.Zero;

        public Cuenta laCuenta { get; set; }
        public int laCuentaId { get; set; }
        public string? laCuentaNombre { get; set; }

        public int elTipoId { get; set; }

        private List<DetalleDTO>? detalleDTOs { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private EmpresaService? EmpresaService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //var url = $"api/tipoconta/full";
            //var losTipos = await Repository.GetAsync<List<TipoConta>>(url);
            var responseHttp = await Repository.GetAsync<List<TipoConta>>("/api/tipoconta/combo");
            tiposConta = responseHttp.Response;
            poliza.Fecha = DateTime.UtcNow;
        }

        private async Task TipoChanged(ChangeEventArgs e)
        {
            var selectedTipoId = Convert.ToInt32(e.Value!);
            poliza.TipoId = selectedTipoId;
            var selectedTipo = tiposConta?.FirstOrDefault(t => t.Id == selectedTipoId);
            elTipoId = selectedTipoId;
            //VerificarDocumentoAsync();
        }

        private async Task VerificarDocumentoAsync()
        {
            if (string.IsNullOrWhiteSpace(poliza.Documento) || poliza.TipoId == 0)
                return;

            var url = $"api/poliza/existe?empresaId={EmpresaId}&documento={poliza.Documento}&tipoId={poliza.TipoId}";

            var responseHttp = await Repository.GetAsync<Poliza>(url);

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    await SweetAlertService.FireAsync("Advertencia", "Este documento con este tipo ya existe.", SweetAlertIcon.Warning);
                    return;
                }
            }
            else
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
        }

        private async Task BuscarCuenta()
        {
            var url = $"api/cuenta/codigo?empresaId={EmpresaId}&codigoCuenta={elCodigo}";

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
            laCuenta = responseHttp.Response;
            laCuentaNombre = responseHttp.Response.Nombre;
            laCuentaId = responseHttp.Response.Id;
            StateHasChanged();
        }

        private async Task CreateAsync()
        {
            var polizaNueva = new Poliza
            {
                Documento = poliza.Documento,
                TipoId = poliza.TipoId,
                Fecha = poliza.Fecha,
                ElMes = poliza.Fecha.ToString("MM/yyyy"),
                Aquien = poliza.Aquien,
                Porque = poliza.Porque,
                Comentario = poliza.Comentario,
                EmpresaId = EmpresaId,
                Origen = "Conta",
                FechaOperado = DateTime.UtcNow,

                Detalles = poliza.Detalles.Select(d => new Detalle
                {
                    EmpresaId = EmpresaId,
                    PolizaId = poliza.Id,
                    TipoId = poliza.TipoId,
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

        private async Task AgregarDetalle()
        {
            losDetalles.Codigo = elCodigo.ToString();
            losDetalles.Debe = (decimal)alDebe;
            losDetalles.Haber = (decimal)alHaber;
            losDetalles.CuentaId = laCuentaId;
            losDetalles.Cuenta = laCuenta;
            if (poliza.Detalles == null)
            {
                poliza.Detalles = new List<DetalleDTO>();
            }

            // Agrega el nuevo detalle a la colección
            poliza.Detalles.Add(losDetalles);
            // Reinicia los detalles
            losDetalles = new DetalleDTO();
            laCuentaNombre = string.Empty;
            elCodigo = string.Empty;
            alDebe = 0;
            alHaber = 0;
            StateHasChanged();
        }

        private async Task EliminarDetalle(int Id)
        {
            var detalle = poliza.Detalles.FirstOrDefault(d => d.Id == Id);
            if (detalle != null)
            {
                poliza.Detalles.Remove(detalle);
            }
        }

        private async Task Cancelar()
        {
            Return();
        }
    }
}