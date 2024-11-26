using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebJar.Shared.Entities;

namespace WebJar.Shared.Servicios
{
    public class EmpresaService
    {
        public Empresa EmpresaSeleccionada { get; private set; } = null!;

        public event Action? OnChange;

        public void SeleccionarEmpresa(Empresa empresa)
        {
            EmpresaSeleccionada = empresa;
            NotificarCambio();
        }

        private void NotificarCambio() => OnChange?.Invoke();
    }
}