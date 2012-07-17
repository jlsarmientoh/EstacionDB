using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class VentaTurnoVO
    {
        private DateTime fecha; //[FECHA]

        public virtual DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        private string producto; //[Producto]

        public virtual string Producto
        {
            get { return producto; }
            set { producto = value; }
        }
        private long tiquete; //[Tiquete_Nro]

        public virtual long Tiquete
        {
            get { return tiquete; }
            set { tiquete = value; }
        }
        
        private double total; //[TOTAL]

        public virtual double Total
        {
            get { return total; }
            set { total = value; }
        }
        private int isla; //[Isla]

        public virtual int Isla
        {
            get { return isla; }
            set { isla = value; }
        }
        private int turno; //[Turno]

        public virtual int Turno
        {
            get { return turno; }
            set { turno = value; }
        }

        private long codEmpleado;

        public virtual long CodEmpleado
        {
            get { return codEmpleado; }
            set { codEmpleado = value; }
        }

        private double galones;

        public virtual double Galones
        {
            get { return galones; }
            set { galones = value; }
        }

    }
}
