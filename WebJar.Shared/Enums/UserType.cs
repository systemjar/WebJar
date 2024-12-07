using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Shared.Enums
{
    public enum UserType
    {
        [Description("Administrador")] Admin,

        [Description("Contabilidad")] Conta,

        [Description("Inventarios")] Inve,

        [Description("Cuentas por Pagar")] PorPagar,

        [Description("Cuentas por Cobrar")] PorCobrar,

        [Description("Iva")] Iva,

        [Description("Activos Fijos")] Activos,

        [Description("Facturacion")] Facturacion,

        [Description("Punto de Venta")] Punto,

        [Description("Invitado")] Guest
    }
}