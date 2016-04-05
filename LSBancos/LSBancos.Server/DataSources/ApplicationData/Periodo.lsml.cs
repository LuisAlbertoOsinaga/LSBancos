using Microsoft.LightSwitch;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LightSwitchApplication
{
    public partial class Periodo
    {
        partial void Año_Validate(EntityValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Error-Message>");
            int anio = 0;
            int.TryParse(this.Año, out anio);
            if (anio < 1900 || anio > 3000)
                results.AddPropertyError("Año fuera de rango!", this.Details.Properties.Año);
        }

        partial void Concepto_Changed()
        {
            if (!string.IsNullOrEmpty(this.Concepto))
                this.Concepto = this.Concepto.Trim().ToUpper();
        }
    }
}