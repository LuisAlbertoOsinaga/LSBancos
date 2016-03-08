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
            {
                if (Bancos.SelectedItem.Details.EntityState == EntityState.Added)
                {
                    Bancos.RemoveSelected();
                    this.Refresh();
                }
                else
                    Bancos.SelectedItem.Delete();
            }
        }

        partial void CuentasDeleteSelected_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void CuentasDeleteSelected_Execute()
        {
            MessageBoxResult result = this.ShowMessageBox(string.Format("Desea eliminar la Cuenta '{0} - {1} - {2}' ?",
                                                                            CuentaBancos.SelectedItem.Banco,
                                                                            CuentaBancos.SelectedItem.Nombre,
                                                                            CuentaBancos.SelectedItem.Moneda),
                                                                        "CONFIRMACION", MessageBoxOption.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (CuentaBancos.SelectedItem.Details.EntityState == EntityState.Added)
                {
                    CuentaBancos.RemoveSelected();
                    this.Refresh();
                }
                else
                    CuentaBancos.SelectedItem.Delete();
            }
        }
    }
}