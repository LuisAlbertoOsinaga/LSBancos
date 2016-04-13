using System.Collections.Specialized;
using Microsoft.LightSwitch.Presentation.Extensions;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System;

using LightSwitchApplication.UserCode.Shared;

namespace LightSwitchApplication
{
    public partial class ChequesSolicitud
    {
        #region Propiedades

        IContentItemProxy btnNroCheque  ;
        IContentItemProxy comboGiradores;

        #endregion

        #region Metodos Auxiliares

        void Bindings()
        {
            comboGiradores.SetBinding(System.Windows.Controls.ComboBox.ItemsSourceProperty, "Screen.Giradores", System.Windows.Data.BindingMode.OneWay);
            comboGiradores.SetBinding(System.Windows.Controls.ComboBox.SelectedItemProperty, "Screen.Giradores.SelectedItem", System.Windows.Data.BindingMode.TwoWay);
        }

        void Findings()
        {
            btnNroCheque = this.FindControl("BtnNroCheque");
            comboGiradores = this.FindControl("ComboGiradores");
        }

        void Init()
        {
            Secuencia secuencia = (from Secuencia sec in this.DataWorkspace.ApplicationData.Secuencias
                                   where sec.Categoria == "CHEQUES" && sec.Clave == "SOLICITUD NRO"
                                   select sec).FirstOrDefault();
            SolicitudNro = secuencia != null ? ServicioSecuencia.GetNro(this.DataWorkspace, secuencia.Id) : 0;
            FechaSolicitud = DateTime.Now;
            btnNroCheque.IsVisible = false;
        }

        #endregion

        #region Metodos Autogenerados

        partial void ChequesSolicitud_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            Findings();
            Init();
            Bindings();
        }

        partial void ChequesSolicitud_Saving(ref bool handled)
        {
            foreach (var cheque in Cheques)
            {
                if (cheque.Details.EntityState == EntityState.Added && string.IsNullOrEmpty(cheque.Nro))
                {
                    Cheques.SelectedItem = cheque;
                    Cheques.DeleteSelected();
                    continue;
                }
                cheque.CuentaBanco = Cuentas.SelectedItem;
                cheque.Estado = "S";    // S-Solicitado
                cheque.FechaSolicitud = FechaSolicitud;
                cheque.Girador = Giradores.SelectedItem;
                cheque.Solicitador = Application.Current.User.Name;
                cheque.SolicitudNro = SolicitudNro.GetValueOrDefault();
            }
        }

        partial void Cuentas_SelectionChanged()
        {
            Cheque cheque = null;
            if(Cuentas.SelectedItem != null)
            {
                cheque = (from Cheque chq in this.DataWorkspace.ApplicationData.Cheques
                          where chq.CuentaBanco.Id == Cuentas.SelectedItem.Id
                          orderby chq.Nro
                          select chq).LastOrDefault();
            }
            UltimoCheque = cheque != null ? cheque.Nro : "0";
        }

        partial void Cheques_SelectionChanged()
        {
            if (Cheques.SelectedItem != null)
                btnNroCheque.IsVisible = string.IsNullOrEmpty(Cheques.SelectedItem.Nro);
            CantidadCheques = Cheques.Count;
            Total = Cheques.Sum(x => x.Monto);
        }

        partial void NumeroCheque_Execute()
        {
            if(Cheques.Count > 1)
            {
                string nroChequeUlt = Cheques.ElementAt(Cheques.Count - 2).Nro;
                int nroCheque = 0;
                if (int.TryParse(nroChequeUlt, out nroCheque))
                    Cheques.SelectedItem.Nro = (++nroCheque).ToString().PadLeft(nroChequeUlt.Length, '0');
                else
                    Cheques.SelectedItem.Nro = UltimoCheque;
            }
            else
            {
                Cheques.SelectedItem.Nro = UltimoCheque;
            }
        }

        #endregion
    }
}