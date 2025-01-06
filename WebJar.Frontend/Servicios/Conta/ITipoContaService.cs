using WebJar.Shared.Entities.Conta;

namespace WebJar.Frontend.Servicios.Conta
{
    public interface ITipoContaService
    {
        Task<List<TipoConta>> ListaTiposConta();
    }
}