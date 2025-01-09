using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using WebJar.Frontend.Repositories;
using WebJar.Shared.Entities.Conta;

namespace WebJar.Frontend.Pages.Conta.Documentos
{
    public partial class DocumentoForm
    {
        private List<TipoConta> tiposConta = new List<TipoConta>();

        private EditContext editContext = null!;

        [EditorRequired, Parameter] public Poliza Poliza { get; set; } = null!;
        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        public bool FormPostedSuccessfully { get; set; } = false;

        protected async Task OnInitializedAsync()
        {
            editContext = new(Poliza);
            var url = "api/tipoconta";
            var responseHttp = await Repository.GetAsync<List<TipoConta>>(url);
            tiposConta = responseHttp.Response;
        }

        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        {
            var formWasEdited = editContext.IsModified();
            if (!formWasEdited || FormPostedSuccessfully)
            {
                return;
            }
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = "¿Deseas abandonar la página y perder los cambios?",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true
            });
            var confirm = !string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }
            context.PreventNavigation();
        }
    }
}