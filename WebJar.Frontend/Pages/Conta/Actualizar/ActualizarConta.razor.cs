using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace WebJar.Frontend.Pages.Conta.Actualizar
{
    public partial class ActualizarConta
    {
        [Parameter] public int EmpresaId { get; set; }

        private string mesSeleccionado { get; set; } = "enero";
        private int añoSeleccionado { get; set; } = DateTime.Now.Year;
        private string mesDosDigitos { get; set; } = "01";
        private string mesActualizar { get; set; }

        private Dictionary<string, string> diccionarioMeses = new Dictionary<string, string>()
        {
            { "enero", "01" },
            { "febrero", "02"},
            { "marzo", "03" },
            { "abril", "04" },
            { "mayo", "05" },
            { "junio", "06 "},
            { "julio", "07" },
            { "agosto", "08" },
            { "septiembre", "09" },
            { "octubre", "10" },
            { "noviembre", "11" },
            { "diciembre", "12" },
        };

        private List<string> meses = DateTimeFormatInfo.CurrentInfo.MonthNames
            .Where(m => !string.IsNullOrWhiteSpace(m))
            .ToList();

        protected override void OnInitialized()
        {
            ActualizarNumeroMes();
        }

        private void ActualizarNumeroMes()
        {
            if (!string.IsNullOrEmpty(mesSeleccionado))
            {
                //numeroMesSeleccionado = diccionarioMeses[mesSeleccionado.ToLower()];
                mesDosDigitos = diccionarioMeses[mesSeleccionado.ToLower()];
                mesActualizar = $"{mesDosDigitos}/{añoSeleccionado}";
            }
        }
    }
}