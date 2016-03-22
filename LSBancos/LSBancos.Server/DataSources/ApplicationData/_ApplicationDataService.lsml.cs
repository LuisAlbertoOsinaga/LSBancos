using Microsoft.LightSwitch.Security.Server;
using Microsoft.LightSwitch;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LightSwitchApplication
{
    public partial class ApplicationDataService
    {
        #region Metodos Auxiliares
        
        void CuentaBancos_Editing(CuentaBanco entity)
        {
            entity.BancoCuenta = string.Format("{0}-{1} - {2}",
                                                entity.Banco.Sigla,
                                                entity.Nombre, 
                                                entity.Moneda.Simbolo);
        }

        void Secuencias_Editing(Secuencia entity)
        {
            entity.CategoriaClave = string.Format("{0} - {1}",
                                                entity.Categoria,
                                                entity.Clave);
        }

        #endregion

        #region Metodos Autogenerados
        partial void CuentaBancos_Inserting(CuentaBanco entity)
        {
            CuentaBancos_Editing(entity);
        }

        partial void CuentaBancos_Updating(CuentaBanco entity)
        {
            CuentaBancos_Editing(entity);
        }

        partial void Secuencias_Inserting(Secuencia entity)
        {
            Secuencias_Editing(entity);
        }

        partial void Secuencias_Updating(Secuencia entity)
        {
            Secuencias_Editing(entity);
        }

        #endregion
    }
}