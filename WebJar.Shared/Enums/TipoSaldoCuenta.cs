using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJar.Shared.Enums
{
    public enum TipoSaldoCuenta
    {
        [Description("Saldo Deudor")] D,

        [Description("Saldo Acreedor")] H
    }
}