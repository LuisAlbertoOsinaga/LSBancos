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
            comboGiradores.SetBinding(System.Windows.Controls.ComboBox.ItemsSourceProperty, 
                                        "Screen.Giradores", System.Windows.Data.BindingMode.TwoWay);
            comboGiradores.SetBinding(System.Windows.Controls.ComboBox.SelectedItemProperty, 
                                        "Screen.Giradores.SelectedItem", System.Windows.Data.BindingMode.TwoWay);
        }

        void Findings()
        {
            btnNroCheque = this.FindControl("BtnNroCheque");
            comboGiradores = this.FindControl("ComboGiradores");
        }

        void Init()
        {
            // Solicitud Nro
            SecuenciaNroSolicitud = (from Secuencia sec in Secuencias
                                   where sec.Categoria == "CHEQUES" && sec.Clave == "SOLICITUD NRO"
                                   select sec).FirstOrDefault();
            SolicitudNro = SecuenciaNroSolicitud != null ? 
                                    ServicioSecuencia.GetNro(this.DataWorkspace, SecuenciaNroSolicitud.Id) : 0;
            SolicitudNroStr = SolicitudNro.ToString().PadLeft(SecuenciaNroSolicitud.Digitos.GetValueOrDefault(), '0');

            // Fecha Solicitud
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

        partial void ChequesSolicitados_Loaded(bool succeeded)
        {
            if (ChequesSolicitados.Count == 0)
            {
                SolicitudNro = ServicioSecuencia.GetNro(this.DataWorkspace, SecuenciaNroSolicitud.Id);
                SolicitudNroStr = SolicitudNro.ToString()
                                    .PadLeft(SecuenciaNroSolicitud.Digitos.GetValueOrDefault(), '0');
                FechaSolicitud = DateTime.Now;

                // Ultimo cheque
                Cheque chequeUltimo = null;
                if (Cuentas.SelectedItem != null)
                {
                    chequeUltimo = (from Cheque chq in this.DataWorkspace.ApplicationData.Cheques
                              where chq.CuentaBanco.Id == Cuentas.SelectedItem.Id
                              orderby chq.Nro
                              select chq).LastOrDefault();
                }
                UltimoCheque = chequeUltimo != null ? chequeUltimo.Nro : "0";
            }
            else
            {
                SolicitudNro = ChequesSolicitados.Last().SolicitudNro;
                SolicitudNroStr = SolicitudNro.ToString()
                                    .PadLeft(SecuenciaNroSolicitud.Digitos.GetValueOrDefault(), '0');
                FechaSolicitud = ChequesSolicitados.Last().FechaSolicitud.GetValueOrDefault();
                UltimoCheque = ChequesSolicitados.Last().Nro;
            }
        }

        partial void ChequesSolicitud_Saving(ref bool handled)
        {
            foreach (var cheque in ChequesSolicitados)
            {
                if (cheque.Details.EntityState == EntityState.Added && string.IsNullOrEmpty(cheque.Nro))
                {
                    ChequesSolicitados.SelectedItem = cheque;
                    ChequesSolicitados.DeleteSelected();
                    continue;
                }
                cheque.CuentaBanco = Cuentas.SelectedItem;
                if(string.IsNullOrEmpty(cheque.Estado))
                    cheque.Estado = "S";    // S-Solicitado
                cheque.FechaSolicitud = FechaSolicitud;
                cheque.Girador = Giradores.SelectedItem;
                cheque.Solicitador = Application.Current.User.Name;
                cheque.SolicitudNro = int.Parse(SolicitudNroStr);
            }
        }

        partial void ChequesSolicitados_SelectionChanged()
        {
            if (ChequesSolicitados.SelectedItem != null)
                btnNroCheque.IsVisible = string.IsNullOrEmpty(ChequesSolicitados.SelectedItem.Nro);
            CantidadCheques = ChequesSolicitados.Count;
            Total = ChequesSolicitados.Sum(x => x.Monto);
        }

        partial void Enviar_Execute()
        {
            CuentaBanco ctaSelected = Cuentas.SelectedItem;
            for (int i = 0; i < ChequesSolicitados.Count; i++)
                ChequesSolicitados.ElementAt(i).Estado = "A";    // A-Aprobado
            SolicitudNro = ServicioSecuencia.PeekNro(this.DataWorkspace, SecuenciaNroSolicitud.Id);
            SolicitudNroStr = SolicitudNro.ToString().PadLeft(SecuenciaNroSolicitud.Digitos.GetValueOrDefault(), '0');
            this.Save();
            this.ShowMessageBox("Envío realizado!", "CONFIRMACION", MessageBoxOption.Ok);
            this.Refresh();
            Cuentas.SelectedItem = ctaSelected;
        }

        partial void Giradores_SelectionChanged()
        {
            if (Giradores.Count > 0 && Giradores.SelectedItem == null)
                Giradores.SelectedItem = Giradores.FirstOrDefault();
        }

        partial void NumeroCheque_Execute()
        {
            if (ChequesSolicitados.Count > 1)
            {
                string nroChequeUlt = ChequesSolicitados.ElementAt(ChequesSolicitados.Count - 2).Nro;
                int nroCheque = 0;
                if (int.TryParse(nroChequeUlt, out nroCheque))
                    ChequesSolicitados.SelectedItem.Nro = (++nroCheque).ToString().PadLeft(nroChequeUlt.Length, '0');
                else
                    ChequesSolicitados.SelectedItem.Nro = UltimoCheque;
            }
            else if (ChequesSolicitados.Count == 1)
            {
                int siguienteCheque = 0;
                if (int.TryParse(UltimoCheque, out siguienteCheque))
                    ChequesSolicitados.SelectedItem.Nro = (++siguienteCheque).ToString().PadLeft(UltimoCheque.Length, '0');
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