using Microsoft.LightSwitch.Presentation.Extensions;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System;
using System.Windows;

namespace LightSwitchApplication
{
    public partial class MonedasLista
    {
        partial void MonedaListAddAndEditNew_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void MonedaListAddAndEditNew_Execute()
        {
            Application.ShowMonedaNueva();
        }

        partial void MonedaListDeleteSelected_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void MonedaListDeleteSelected_Execute()
        {
            MessageBoxResult result = this.ShowMessageBox(string.Format("Desea eliminar la Moneda '{0} - {1}' ?",
                                                                            Monedas.SelectedItem.Simbolo,
                                                                            Monedas.SelectedItem.Nombre),
                                                                        "CONFIRMACION", MessageBoxOption.YesNo);
            if (result == MessageBoxResult.Yes)
                Monedas.SelectedItem.Delete();
        }
    }
}