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
            SecuenciaNroSolicitud = (from Secuencia sec in Secuencias
                                   where sec.Categoria == "CHEQUES" && sec.Clave == "SOLICITUD NRO"
                                   select sec).FirstOrDefault();
            SolicitudNro = SecuenciaNroSolicitud != null ? ServicioSecuencia.GetNro(this.DataWorkspace, SecuenciaNroSolicitud.Id) : 0;
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

        partial void Cheques_Loaded(bool succeeded)
        {
            if (Cheques.Count == 0)
            {
                SolicitudNro = ServicioSecuencia.PeekNro(this.DataWorkspace, SecuenciaNroSolicitud.Id);
                FechaSolicitud = DateTime.Now;
            }
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
                if(string.IsNullOrEmpty(cheque.Estado))
                    cheque.Estado = "S";    // S-Solicitado
                cheque.FechaSolicitud = FechaSolicitud;
                cheque.Girador = Giradores.SelectedItem;
                cheque.Solicitador = Application.Current.User.Name;
                cheque.SolicitudNro = SolicitudNro.GetValueOrDefault();
            }
        }

        partial void Cuentas_SelectionChanged()
        {
            // Ultimo cheque
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
            {
                    FechaSolicitud = Cheques.SelectedItem.FechaSolicitud.GetValueOrDefault();
                    btnNroCheque.IsVisible = string.IsNullOrEmpty(Cheques.SelectedItem.Nro);
            }
            CantidadCheques = Cheques.Count;
            Total = Cheques.Sum(x => x.Monto);
        }

        partial void Giradores_Loaded(bool succeeded)
        {
            if (Giradores.Count > 0)
                Giradores.SelectedItem = Giradores.FirstOrDefault();
        }

        partial void Enviar_Execute()
        {
            CuentaBanco ctaSelected = Cuentas.SelectedItem;
            foreach (var cheque in Cheques)
                cheque.Estado = "A";    // A-Aprobado
            this.Save();
            this.Refresh();
            Cuentas.SelectedItem = ctaSelected;
        }

        partial void NumeroCheque_Execute()
        {
            if (Cheques.Count > 1)
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
                int siguienteCheque = 0;
                if (int.TryParse(UltimoCheque, out siguienteCheque))
                    Cheques.SelectedItem.Nro = (++siguienteCheque).ToString().PadLeft(UltimoCheque.Length, '0');
            }
        }

        partial void NuevoBeneficiario_Execute()
        {
            Application.ShowBeneficiarioNuevo();
        }

        partial void NuevoConcepto_Execute()
        {
            Application.ShowConceptoNuevo();
        }

        partial void NuevoRubro_Execute()
        {
            Application.ShowRubroNuevo();
        }

        #endregion
    }
}