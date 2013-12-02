using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class CierreEfectivoVO
    {
        private long idCierre;

        public virtual long IdCierre
        {
            get { return idCierre; }
            set { idCierre = value; }
        }
        private DateTime fecha;

        public virtual DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        private double efectivo;

        public virtual double Efectivo
        {
            get { return efectivo; }
            set { efectivo = value; }
        }
        private double egresos;

        public virtual double Egresos
        {
            get { return egresos; }
            set { egresos = value; }
        }
        private bool estado;

        public virtual bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }
    }
}
