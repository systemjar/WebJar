using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using WebJar.Frontend.Repositories;
using WebJar.Shared.Entities.Conta;

namespace WebJar.Frontend.Servicios.Conta
{
    public class TipoContaService : ITipoContaService
    {
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        private readonly HttpClient _httpClient;

        public TipoContaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TipoConta>> ListaTiposConta()
        {
            var responseHttp = await Repository.GetAsync<List<TipoConta>>("/api/tipoconta");

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return null;
            }
            return responseHttp.Response!;
        }
    }
}