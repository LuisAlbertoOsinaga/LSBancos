using Microsoft.LightSwitch;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LightSwitchApplication
{
    public partial class Cheque
    {
        partial void CuentaBancoChequeComp_Compute(ref string result)
        {
            result = string.Format("{0} - {1}", this.CuentaBanco, this.Nro);
            CuentaBancoCheque = result;
        }
    }
}