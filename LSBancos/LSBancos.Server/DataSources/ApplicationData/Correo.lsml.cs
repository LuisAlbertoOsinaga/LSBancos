using Microsoft.LightSwitch;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LightSwitchApplication
{
    public partial class Correo
    {
        partial void RolDestinatarioDireccion_Compute(ref string result)
        {
            result = this.RolCorreo.Rol + "-" + this.Destinatario + "-" 
                            + this.Dirección;
        }
    }
}