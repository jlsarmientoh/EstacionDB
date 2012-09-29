using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class SobretasaVO
    {
        private long id;//[ID]

        public virtual long Id
        {
            get { return id; }
            set { id = value; }
        }
        private int mes;//[MES_VIGENCIA]

        public virtual int Mes
        {
            get { return mes; }
            set { mes = value; }
        }
        private int anio;//[ANIO_VIGENCIA]

        public virtual int Anio
        {
            get { return anio; }
            set { anio = value; }
        }
        private int idProducto;//[ID_PRODUCTO]

        public virtual int IdProducto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }
        private double precioBase;//[PRECIO_BASE]

        public virtual double PrecioBase
        {
            get { return precioBase; }
            set { precioBase = value; }
        }
        private double fletes;//[FLETES]

        public virtual double Fletes
        {
            get { return fletes; }
            set { fletes = value; }
        }
        private double soldicom;//[SOLDICOM]

        public virtual double Soldicom
        {
            get { return soldicom; }
            set { soldicom = value; }
        }
        private double sobretasa;//[SOBRETASA]

        public virtual double Sobretasa
        {
            get { return sobretasa; }
            set { sobretasa = value; }
        }
        private double venta;//[VENTA]

        public virtual double Venta
        {
            get { return venta; }
            set { venta = value; }
        }
    }
}
