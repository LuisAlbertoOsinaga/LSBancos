using Microsoft.LightSwitch;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LightSwitchApplication
{
    public partial class Cheque
    {
        partial void CuentaBancoCheque_Compute(ref string result)
        {
            result = this.CuentaBanco.BancoCuenta + "-" + this.Nro;
        }
    }
}