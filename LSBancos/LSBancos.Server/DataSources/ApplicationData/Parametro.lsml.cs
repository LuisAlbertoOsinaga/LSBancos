using Microsoft.LightSwitch;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LightSwitchApplication
{
    public partial class Parametro
    {
        partial void CategoriaClaveValor_Compute(ref string result)
        {
            result = this.Categoria + "-" + this.Clave + "-"
                        + this.Valor;
        }

        partial void Categoria_Changed()
        {
            if (!string.IsNullOrEmpty(this.Categoria))
                this.Categoria = this.Categoria.Trim().ToUpper();
        }

        partial void Clave_Changed()
        {
            if (!string.IsNullOrEmpty(this.Clave))
                this.Clave = this.Clave.Trim().ToUpper();
        }
    }
}