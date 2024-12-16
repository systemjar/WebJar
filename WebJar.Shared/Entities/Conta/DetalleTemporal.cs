using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJar.Shared.Entities.Conta
{
    public class DetalleTemporal
    {
        public int Id { get; set; }
        public string Documento { get; set; }
        public string Tipo { get; set; }
        public string Codigo { get; set; }
        public string Cuenta { get; set; }
        public string Debe { get; set; }
        public string Haber { get; set; }
        public string Contras { get; set; }
        public string Factura { get; set; }
        public string Serie { get; set; }
        public string Origen { get; set; }

        public string ElMes;
    }
}