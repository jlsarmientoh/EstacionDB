using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.DTO;

namespace EstacionDB.Utilidades
{
    public static class Utilidades
    {
        public static string cadenaConexion;
        public static string appCadenaConexion;
        public static string configServ;
        public static string configExpo;
        public static string nombreVistaVentas;
        public static string nombreVistaLecturas;
        public static string nombreTablaEmpleados;
        public static string rutaPrincipalExport;        
        public static string separador;
        public static bool usarEncabezados;        
        public static int codigoSuper;
        public static int codigoCorriente;
        public static int codigoDiesel;
        public static string grupo1;
        public static string grupo2;
        public static int multiplicarX;
        public static string TipoMovimiento;
        public static string NatutalezaDebito;
        public static string NatutalezaCredito;
        public static string NitEDS;
        public static string NitSodexo;
        public static string NitBigPass;
        public static string NitTicketTronik;
        public static string CuentaCredito;
        public static string CuentaEfectivo;
        public static string CuentaSodexo;
        public static string CuentaBigPass;
        public static string CuentaTarjetas;
        public static string CuentaOtros;
        public static string CuentaTarjetaPlus;
        public static string CuentaTicketTronik;
        public static string CuentaSobretasaCorriente;
        public static string CuentaSobretasaSuper;
        public static string CuentaSobretasaDiesel;
        public static string CuentaVentaCorriente;
        public static string CuentaVentaSuper;
        public static string CuentaVentaDiesel;
        public static string CuentaAjuste;
        public static Boolean HomologarNits;

        public static NitDTO formatearNit(String nit)
        {
            NitDTO resultado = new NitDTO();
            string numericalNit;

            StringBuilder origin = new StringBuilder();
            char[] charArray = nit.ToCharArray();

            // se tienen en cuenta solo los digitos numéricos
            foreach(char c in charArray)
            {
                int flag;
                if (!c.Equals('-') && !c.Equals(',') && !c.Equals('.'))
                {
                    origin.Append(c);
                }
            }
            
            numericalNit = origin.ToString();

            if (numericalNit.Length <= 9)
            {
                resultado.Nit = numericalNit;
                resultado.DigitoVerfificacion = "0";
            }

            if (numericalNit.Length > 9) //Cuando se tiene el nit y el digito de verificacion en el mismo campo
            {
                resultado.Nit = numericalNit.Substring(0, 9);
                resultado.DigitoVerfificacion = numericalNit.Substring(9, 1);
            }
            if (numericalNit.Length > 10) //Cuando ademas se tienen consecutivos adicionales
            {
                resultado.Nit = numericalNit.Substring(0, 9);
                resultado.DigitoVerfificacion = numericalNit.Substring(9, 1);
            }
            //Se guarda el nit original de servP en el campo codigo para homologar con Expo
            resultado.Codigo = numericalNit;

            return resultado;
        }
    }

}
