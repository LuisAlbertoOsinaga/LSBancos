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
    public partial class BeneficiariosLista
    {
        partial void BeneficiarioListAddAndEditNew_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void BeneficiarioListAddAndEditNew_Execute()
        {
            Application.ShowBeneficiarioNuevo();
        }

        partial void BeneficiarioListDeleteSelected_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void BeneficiarioListDeleteSelected_Execute()
        {
            MessageBoxResult result = this.ShowMessageBox(string.Format("Desea eliminar el Beneficiario '{0}' ?",
                                                                            Beneficiarios.SelectedItem.Nombre),
                                                                        "CONFIRMACION", MessageBoxOption.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (Beneficiarios.SelectedItem.Details.EntityState == EntityState.Added)
                {
                    Beneficiarios.RemoveSelected();
                    this.Refresh();
                }
                else
                    Beneficiarios.SelectedItem.Delete();
            }
        }
    }
}