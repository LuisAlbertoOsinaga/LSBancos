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
    public partial class RubrosLista
    {
        partial void RubroListAddAndEditNew_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void RubroListAddAndEditNew_Execute()
        {
            Application.ShowRubroNuevo();
        }

        partial void RubroListDeleteSelected_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void RubroListDeleteSelected_Execute()
        {
            MessageBoxResult result = this.ShowMessageBox(string.Format("Desea eliminar el Rubro '{0}' ?",
                                                                            Rubros.SelectedItem.Nombre),
                                                                        "CONFIRMACION", MessageBoxOption.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (Rubros.SelectedItem.Details.EntityState == EntityState.Added)
                {
                    Rubros.RemoveSelected();
                    this.Refresh();
                }
                else
                    Rubros.SelectedItem.Delete();
            }
        }
    }
}