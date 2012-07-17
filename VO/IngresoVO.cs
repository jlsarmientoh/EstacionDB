using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class IngresoVO
    {
        private long idIngreso = 0;

        public virtual long IdIngreso
        {
            get { return idIngreso; }
            set { idIngreso = value; }
        }
        private long documento = 1;

        public virtual long Documento
        {
            get { return documento; }
            set { documento = value; }
        }
        private long numConsignacion;

        public virtual long NumConsignacion
        {
            get { return numConsignacion; }
            set { numConsignacion = value; }
        }
        private long idBanco = 1;

        public virtual long IdBanco
        {
            get { return idBanco; }
            set { idBanco = value; }
        }
        private string banco;

        public virtual string Banco
        {
            get { return banco; }
            set { banco = value; }
        }
        private double valor;

        public virtual double Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        private DateTime fecha;

        public virtual DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
    }
}
