using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using WebJar.Frontend.Repositories;
using WebJar.Shared.DTOs;
using WebJar.Shared.DTOs.Conta;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Servicios;
using static MudBlazor.Colors;

namespace WebJar.Frontend.Pages.Conta.Documentos
{
    public partial class DocumentosCreate
    {
        [Parameter] public int EmpresaId { get; set; }

        public PolizaDTO poliza = new();
        public DetalleDTO losDetalles = new DetalleDTO();

        private DocumentoForm? documentoForm;
        public List<TipoConta>? tiposConta { get; set; }

        private List<Cuenta>? cuentas { get; set; }
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
            poliza.Fecha = DateTime.Today;
        }

        private void TipoChanged(ChangeEventArgs e)
        {
            var selectedTipo = Convert.ToInt32(e.Value!);
            poliza.TipoId = selectedTipo;
        }

        private async Task CreateAsync()
        {
            var polizaNueva = new Poliza
            {
                Documento = poliza.Documento,
                TipoId = poliza.TipoId,
                Fecha = poliza.Fecha,
                Aquien = poliza.Aquien,
                Porque = poliza.Porque,
                Comentario = poliza.Comentario,
                EmpresaId = EmpresaId,
            };

            //var url = $"api/cuenta/buscar?empresaId={EmpresaId}&cuentaCodigo={RecordsNumber}";

            //var detalleNuevo = new Detalle
            //{
            //    DocumentoId = elDocumentoId,

            //    TipoId = poliza.TipoId,
            //    CuentaId = cuenta.Id,
            //    Debe = poliza.Detalles.Debe,
            //    Haber = poliza.Detalles.  .Haber,
            //    EmpresaId = 1, // Aquí puedes setear el EmpresaId correspondiente
            //    Documento = poliza,
            //    TipoId = poliza.TipoId // Usa el mismo TipoId que la póliza
            //};

            //poliza.Detalles = new List<Detalle> { detalle };

            //dbContext.Polizas.Add(poliza);
            //await dbContext.SaveChangesAsync();
            NavigationManager.NavigateTo("/");
        }

        private void Return()
        {
            documentoForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/documentos/{poliza.EmpresaId}");
        }

        private void AgregarDetalle()
        {
        }

        private void EliminarDetalle(int Id)
        {
        }

        private void Cancelar()
        {
        }

        private void AsignarCuenta(Detalle detalle)
        {
            var url = "/cuentas/edit/@cuenta.Id";
        }
    }
}