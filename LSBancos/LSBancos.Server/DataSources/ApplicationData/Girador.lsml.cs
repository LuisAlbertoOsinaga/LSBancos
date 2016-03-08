using Microsoft.LightSwitch;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LightSwitchApplication
{
    public partial class Girador
    {
        partial void Nombre_Changed()
        {
            if (!string.IsNullOrEmpty(this.Nombre))
                this.Nombre = this.Nombre.Trim().ToUpper();
        }
    }
}