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
    public partial class GiradoresLista
    {
        partial void GiradorListAddAndEditNew_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void GiradorListAddAndEditNew_Execute()
        {
            Application.ShowGiradorNuevo();
        }

        partial void GiradorListDeleteSelected_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void GiradorListDeleteSelected_Execute()
        {
            MessageBoxResult result = this.ShowMessageBox(string.Format("Desea eliminar el Girador '{0}' ?",
                                                                            Giradores.SelectedItem.Nombre),
                                                                        "CONFIRMACION", MessageBoxOption.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (Giradores.SelectedItem.Details.EntityState == EntityState.Added)
                {
                    Giradores.RemoveSelected();
                    this.Refresh();
                }
                else
                    Giradores.SelectedItem.Delete();
            }
        }
    }
}