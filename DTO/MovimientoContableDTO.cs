using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.DTO
{
    public class MovimientoContableDTO
    {
        private string fecha;

        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        private string tipoDoc;

        public string TipoDoc
        {
            get { return tipoDoc; }
            set { tipoDoc = value; }
        }
        private string doc;

        public string Doc
        {
            get { return doc; }
            set { doc = value; }
        }
        private string nit;

        public string Nit
        {
            get { return nit; }
            set { nit = value; }
        }
        private string cuenta;

        public string Cuenta
        {
            get { return cuenta; }
            set { cuenta = value; }
        }
        private string naturaleza;

        public string Naturaleza
        {
            get { return naturaleza; }
            set { naturaleza = value; }
        }
        private string valor;

        public string Valor
        {
            get { return valor; }
            set { valor = value; }
        }
        private string centroCosto;

        public string CentroCosto
        {
            get { return centroCosto; }
            set { centroCosto = value; }
        }

        private string cuentaBancaria;

        public string CuentaBancaria
        {
            get { return cuentaBancaria; }
            set { cuentaBancaria = value; }
        }
    }
}
