using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightSwitchApplication.UserCode.Shared
{
    class ServicioSecuencia
    {
        #region Métodos

        public static int GetNro(DataWorkspace dw, int secuenciaId, 
                                                    out int nroFinal, out int digitos, bool peek = false)
        {
            // Consigue secuencia
            Secuencia secuencia = dw.ApplicationData.Secuencias_SingleOrDefault(secuenciaId);
            if (secuencia == null)
                throw new ServicioSecuenciaException(string.Format("Error en ServicioSecuencia.GetSecuenciaSiguiente. " +
                                                                    "Secuencia({0})." +
                                                                    " Registro no encontrado!", secuenciaId));

            nroFinal = secuencia.NroFinal;
            digitos = secuencia.Digitos.GetValueOrDefault();
            int nroActual = secuencia.NroActual;
            if (peek)
            {
                secuencia.NroActual++;
            }
            return nroActual;
        }

        public static string GetNroStr(DataWorkspace dw, int secuenciaId, bool peek = false)
        {
            int nroActual;
            int digitos;
            return GetNro(dw, secuenciaId, out nroActual, out digitos, peek)
                        .ToString().PadLeft(digitos, '0');
        }

        public static int PeekNro(DataWorkspace dw, int secuenciaId, out int nroFinal, out int digitos)
        {
            return GetNro(dw, secuenciaId, out nroFinal, out digitos, peek: true);
        }

        public static string PeekNroStr(DataWorkspace dw, int secuenciaId)
        {
            return GetNroStr(dw, secuenciaId, peek: true);
        }

        #endregion
    }

    public class ServicioSecuenciaException : Exception
    {
        public ServicioSecuenciaException() : base() { }
        public ServicioSecuenciaException(string message) : base(message) { }
        public ServicioSecuenciaException(string message, Exception innerException) : base(message, innerException) { }
    }
}
