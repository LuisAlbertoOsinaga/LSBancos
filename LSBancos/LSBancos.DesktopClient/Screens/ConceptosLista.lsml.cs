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
    public partial class ConceptosLista
    {
        partial void ConceptoListAddAndEditNew_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void ConceptoListAddAndEditNew_Execute()
        {
            Application.Current.ShowConceptoNuevo();
        }

        partial void ConceptoListDeleteSelected_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void ConceptoListDeleteSelected_Execute()
        {
            MessageBoxResult result = this.ShowMessageBox(string.Format("Desea eliminar el Concepto '{0}' ?",
                                                                            Conceptos.SelectedItem.Nombre),
                                                                        "CONFIRMACION", MessageBoxOption.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (Conceptos.SelectedItem.Details.EntityState == EntityState.Added)
                {
                    Conceptos.RemoveSelected();
                    this.Refresh();
                }
                else
                    Conceptos.SelectedItem.Delete();
            }
        }
    }
}