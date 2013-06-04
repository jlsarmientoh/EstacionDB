using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class CompraCombustibleVO
    {
        private long idCompra; //[ID_COMPRA]

        public virtual long IdCompra
        {
            get { return idCompra; }
            set { idCompra = value; }
        }
        private long factura; //[FACTURA]

        public virtual long Factura
        {
            get { return factura; }
            set { factura = value; }
        }
        private int idProducto; //[ID_PRODUCTO]

        public virtual int IdProducto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }
        private double galones; //[CANTIDAD_GL]

        public virtual double Galones
        {
            get { return galones; }
            set { galones = value; }
        }
        private double valorUnitario; //[VALOR_UNITARIO]

        public virtual double ValorUnitario
        {
            get { return valorUnitario; }
            set { valorUnitario = value; }
        }
        private double valor; //[VALOR]

        public virtual double Valor
        {
            get { return valor; }
            set { valor = value; }
        }
        private double flete; //[FLETE]

        public virtual double Flete
        {
            get { return flete; }
            set { flete = value; }
        }
        private double soldicom; //[SOLDICOM]

        public virtual double Soldicom
        {
            get { return soldicom; }
            set { soldicom = value; }
        }
        private double impuesto; //[IMPUESTO]

        public virtual double Impuesto
        {
            get { return impuesto; }
            set { impuesto = value; }
        }

        private DateTime fecha; //FECHA

        public virtual DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
    }
}
