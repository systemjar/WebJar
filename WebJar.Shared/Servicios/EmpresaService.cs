using WebJar.Shared.Entities;

namespace WebJar.Shared.Servicios
{
    public class EmpresaService
    {
        public Empresa? EmpresaSeleccionada { get; private set; }

        public void SeleccionarEmpresa(Empresa empresa)
        {
            EmpresaSeleccionada = empresa;
        }
    }
}