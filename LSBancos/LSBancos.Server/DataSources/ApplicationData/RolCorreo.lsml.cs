using Microsoft.LightSwitch;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LightSwitchApplication
{
    public partial class RolCorreo
    {
        partial void Rol_Changed()
        {
            if (!string.IsNullOrEmpty(this.Rol))
                this.Rol = this.Rol.Trim().ToUpper();
        }
    }
}