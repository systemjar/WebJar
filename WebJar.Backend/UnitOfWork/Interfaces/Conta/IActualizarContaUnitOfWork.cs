namespace WebJar.Backend.UnitOfWork.Interfaces.Conta
{
    public interface IActualizarContaUnitOfWork
    {
        Task ActualizarConta(int empresaId, int elMes, int elYear);
    }
}