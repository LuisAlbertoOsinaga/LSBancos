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
    public partial class ChequesSolicitud
    {
        IContentItemProxy comboCuentas;

        void Bindings()
        {
            comboCuentas.SetBinding(System.Windows.Controls.ComboBox.ItemsSourceProperty, 
                                        "Screen.Bancos", 
                                        System.Windows.Data.BindingMode.OneWay);
            comboCuentas.SetBinding(System.Windows.Controls.ComboBox.SelectedItemProperty, 
                                        "Screen.Bancos.SelectedItem", 
                                        System.Windows.Data.BindingMode.TwoWay);
        }
            
        void FindControls()
        {
            comboCuentas = this.FindControl("ComboBancos");
        }


        partial void ChequesSolicitud_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            FindControls();
            Bindings();
        }
    }
}