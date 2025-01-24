namespace WebJar.Backend.Repositories.Interfaces.Conta
{
    public interface IActualizarContaRepository
    {
        Task ActualizarConta(int empresaId, int elMes, int elYear);
    }
}