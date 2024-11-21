using WebJar.Shared.Entities;

namespace WebJar.Frontend.Pages.Conta.TiposConta
{
    public partial class TiposContaIndex
    {
        public List<TipoConta>? LTiposConta { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var responseHppt = await repository.GetAsync<List<TipoConta>>("api/tipoconta");
            LTiposConta = responseHppt.Response!;
        }
    }
}