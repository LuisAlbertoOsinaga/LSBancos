using Microsoft.LightSwitch;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LightSwitchApplication
{
    public partial class CuentaBanco
    {
        partial void BancoCuenta_Compute(ref string result)
        {
            result = this.Banco.Sigla + "-" + this.Nro;
        }

        partial void Nombre_Changed()
        {
            if (!string.IsNullOrEmpty(this.Nombre))
                this.Nombre = this.Nombre.Trim().ToUpper();
        }
    }
}