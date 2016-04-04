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
    public partial class ChequesSolicitados
    {
        #region Propiedades
        #endregion

        #region Metodos Auxiliares
        #endregion

        #region Metodos Autogenerados

        partial void ChequesSolicitados_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Write your code here.

        }

        partial void ChequesDeleteSelected_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void ChequesDeleteSelected_Execute()
        {
            MessageBoxResult result = this.ShowMessageBox(string.Format("Desea eliminar el cheque '{0}' de esta solicitud?",
                                                                            Cheques.SelectedItem.Nro),
                                                                        "CONFIRMACION", MessageBoxOption.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (Cheques.SelectedItem.Details.EntityState == EntityState.Added)
                {
                    Cheques.RemoveSelected();
                    this.Refresh();
                }
                else
                    Cheques.SelectedItem.Delete();
            }
        }

        partial void AnularCheque_Execute()
        {
            this.OpenModalWindow("AnulacionCheque");
        }

        partial void AnulacionCheque_Execute()
        {
            if(!FechaAnulacion.HasValue)
            {
                this.ShowMessageBox("Introduzca fecha de Anulación!");
                return;
            }
            if (string.IsNullOrEmpty(CausaAnulacion))
            {
                this.ShowMessageBox("Introduzca Causa de la Anulación!");
                return;
            }

            Cheques.SelectedItem.Estado = "A";  // A-Anulado
            Cheques.SelectedItem.FechaAnulacion = FechaAnulacion;
            Cheques.SelectedItem.Observaciones = string.Format("|Anulado por: '{0}'", CausaAnulacion);
            this.CloseModalWindow("AnulacionCheque");
        }

        partial void CancelaAnulacion_Execute()
        {
            this.CloseModalWindow("AnulacionCheque");
        }

        partial void ChequesSolicitados_Saved()
        {
            this.Refresh();
        }

        #endregion
    }
}