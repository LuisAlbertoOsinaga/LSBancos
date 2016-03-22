using Microsoft.LightSwitch.Presentation.Extensions;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System;

using LightSwitchApplication.UserCode.Shared;

namespace LightSwitchApplication
{
    public partial class ChequesSolicitud
    {
        #region Propiedades

        IContentItemProxy comboGiradores;

        #endregion

        #region Metodos Auxiliares

        void Bindings()
        {
            comboGiradores.SetBinding(System.Windows.Controls.ComboBox.ItemsSourceProperty, "Screen.Giradores", System.Windows.Data.BindingMode.OneWay);
            comboGiradores.SetBinding(System.Windows.Controls.ComboBox.SelectedItemProperty, "Screen.Giradores.SelectedItem", System.Windows.Data.BindingMode.TwoWay);
        }

        void Findings()
        {
            comboGiradores = this.FindControl("ComboGiradores");
        }

        void Init()
        {
            Secuencia secuencia = (from Secuencia sec in this.DataWorkspace.ApplicationData.Secuencias
                                   where sec.Categoria == "CHEQUES" && sec.Clave == "SOLICITUD NRO"
                                   select sec).FirstOrDefault();
            SolicitudNro = secuencia != null ? ServicioSecuencia.GetNro(this.DataWorkspace, secuencia.Id) : 0;
            FechaSolicitud = DateTime.Now;
        }

        #endregion

        #region Metodos Autogenerados

        partial void ChequesSolicitud_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            Init();
            Findings();
            Bindings();
        }

        partial void Cuentas_SelectionChanged()
        {
            Cheque cheque = null;
            if(Cuentas.SelectedItem != null)
            {
                cheque = (from Cheque chq in this.DataWorkspace.ApplicationData.Cheques
                          where chq.CuentaBanco.Id == Cuentas.SelectedItem.Id
                          orderby chq.Nro
                          select chq).LastOrDefault();
            }
            UltimoCheque = cheque != null ? cheque.Nro : "0";
        }

        #endregion
    }
}