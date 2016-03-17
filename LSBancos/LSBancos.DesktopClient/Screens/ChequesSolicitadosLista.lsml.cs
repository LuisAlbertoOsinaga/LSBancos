using Microsoft.LightSwitch.Presentation.Extensions;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System;

namespace LightSwitchApplication
{
    public partial class ChequesSolicitadosLista
    {
        #region Propiedades

        IContentItemProxy comboCuentas;
        IContentItemProxy comboGiradores;

        #endregion

        #region Metodos Especiales

        void Bindings()
        {
            comboCuentas.SetBinding(System.Windows.Controls.ComboBox.ItemsSourceProperty,
                                        "Screen.Cuentas",
                                        System.Windows.Data.BindingMode.OneWay);
            comboCuentas.SetBinding(System.Windows.Controls.ComboBox.SelectedItemProperty,
                                        "Screen.Cuentas.SelectedItem",
                                        System.Windows.Data.BindingMode.TwoWay);
            comboGiradores.SetBinding(System.Windows.Controls.ComboBox.ItemsSourceProperty, 
                                        "Screen.Giradores", 
                                        System.Windows.Data.BindingMode.OneWay);
            comboGiradores.SetBinding(System.Windows.Controls.ComboBox.SelectedItemProperty, 
                                        "Screen.Giradores.SelectedItem", 
                                        System.Windows.Data.BindingMode.TwoWay);
        }

        void Findings()
        {
            comboCuentas = this.FindControl("ComboCuentas");
            comboGiradores = this.FindControl("ComboGiradores");
        }

        void Inits()
        {
            FechaSolicitud = DateTime.Now;
        }

        #endregion

        #region Metodos Autogenerados

        partial void ChequesSolicitadosLista_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            Inits();
            Findings();
            Bindings();
        }

        #endregion
    }
}