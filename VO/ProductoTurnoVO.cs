using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class ProductoTurnoVO
    {
        private long id;

        public virtual long Id
        {
            get { return id; }
            set { id = value; }
        }
        private DateTime fecha;

        public virtual DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        private long isla;

        public virtual long Isla
        {
            get { return isla; }
            set { isla = value; }
        }
        private long turno;

        public virtual long Turno
        {
            get { return turno; }
            set { turno = value; }
        }
        private double galones;

        public virtual double Galones
        {
            get { return galones; }
            set { galones = value; }
        }
        private double valor;

        public virtual double Valor
        {
            get { return valor; }
            set { valor = value; }
        }
        private string producto;

        public virtual string Producto
        {
            get { return producto; }
            set { producto = value; }
        }
    }
}
