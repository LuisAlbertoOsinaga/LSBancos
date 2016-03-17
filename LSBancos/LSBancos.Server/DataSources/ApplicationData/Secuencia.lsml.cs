using Microsoft.LightSwitch;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LightSwitchApplication
{
    public partial class Secuencia
    {
        partial void NroActual_Validate(EntityValidationResultsBuilder results)
        {
            if (this.NroActual < this.NroInicial)
                results.AddPropertyError("NroActual no puede ser menor que NroInicial", 
                                            this.Details.Properties.NroActual );
            if (this.NroActual > this.NroFinal)
                results.AddPropertyError("NroActual no puede ser mayor que NroFinal",
                                            this.Details.Properties.NroActual);
        }

        partial void NroFinal_Validate(EntityValidationResultsBuilder results)
        {
            if (this.NroFinal < this.NroInicial)
                results.AddPropertyError("NroFinal no puede ser menor que NroInicial",
                                            this.Details.Properties.NroFinal);
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