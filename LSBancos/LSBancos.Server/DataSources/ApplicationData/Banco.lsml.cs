using Microsoft.LightSwitch;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LightSwitchApplication
{
    public partial class Banco
    {
        partial void Sigla_Changed()
        {
            if (!string.IsNullOrEmpty(this.Sigla))
                this.Sigla = this.Sigla.Trim().ToUpper();
        }

        partial void Nombre_Changed()
        {
            if (!string.IsNullOrEmpty(this.Nombre))
                this.Nombre = this.Nombre.Trim().ToUpper();
        }
    }
}