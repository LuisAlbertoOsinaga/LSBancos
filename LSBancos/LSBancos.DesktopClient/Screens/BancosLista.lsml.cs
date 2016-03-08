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
    public partial class BancosLista
    {
        partial void BancoListAddAndEditNew_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void BancoListAddAndEditNew_Execute()
        {
            Application.ShowBancoNuevo();
        }

        partial void BancoListDeleteSelected_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void BancoListDeleteSelected_Execute()
        {
            MessageBoxResult result = this.ShowMessageBox(string.Format("Desea eliminar el Banco '{0} - {1}' ?", 
                                                                            Bancos.SelectedItem.Sigla,
                                                                            Bancos.SelectedItem.Nombre), 
                                                                        "CONFIRMACION", MessageBoxOption.YesNo);
            if (result == MessageBoxResult.Yes)
                Bancos.SelectedItem.Delete(); 
        }
    }
}