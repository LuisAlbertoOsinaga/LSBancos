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
    public partial class SecuenciasLista
    {
        partial void SecuenciaListAddAndEditNew_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void SecuenciaListAddAndEditNew_Execute()
        {
            Application.ShowSecuenciaNueva();
        }

        partial void SecuenciaListDeleteSelected_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void SecuenciaListDeleteSelected_Execute()
        {
            MessageBoxResult result = this.ShowMessageBox(string.Format("Desea eliminar el la Secuencia '{0}' ?",
                                                                            Secuencias.SelectedItem.CategoriaClave),
                                                                        "CONFIRMACION", MessageBoxOption.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (Secuencias.SelectedItem.Details.EntityState == EntityState.Added)
                {
                    Secuencias.RemoveSelected();
                    this.Refresh();
                }
                else
                    Secuencias.SelectedItem.Delete();
            }
        }
    }
}