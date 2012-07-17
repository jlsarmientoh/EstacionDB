using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class CierreVentasVO
    {
        //[ID_CIERRE] [bigint] IDENTITY(1,1) NOT NULL,
        private long idCierre = 0;

        public virtual long IdCierre
        {
            get { return idCierre; }
            set { idCierre = value; }
        }
	    //[FECHA] [datetime] NULL,
        private DateTime fecha;

        public virtual DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
	    //[TOTAL_VENTAS] [float] NULL,
        private double totalVentas = 0;

        public virtual double TotalVentas
        {
            get { return totalVentas; }
            set { totalVentas = value; }
        }
	    //[CREDITOS] [float] NULL,
        private double creditos = 0;

        public virtual double Creditos
        {
            get { return creditos; }
            set { creditos = value; }
        }
	    //[EFECTIVO] [float] NULL,
        private double efectivo = 0;

        public virtual double Efectivo
        {
            get { return efectivo; }
            set { efectivo = value; }
        }
	    //[SODEXO] [float] NULL,
        private double sodexo = 0;

        public virtual double Sodexo
        {
            get { return sodexo; }
            set { sodexo = value; }
        }
	    //[BIG_PASS] [float] NULL,
        private double bigPass = 0;

        public virtual double BigPass
        {
            get { return bigPass; }
            set { bigPass = value; }
        }
	    //[OTROS] [float] NULL,
        private double otros = 0;

        public virtual double Otros
        {
            get { return otros; }
            set { otros = value; }
        }
	    //[TURNO] [bigint] NULL,
        private long turno = 0;

        public virtual long Turno
        {
            get { return turno; }
            set { turno = value; }
        }
	    //[ISLA] [bigint] NULL,
        private long isla = 0;

        public virtual long Isla
        {
            get { return isla; }
            set { isla = value; }
        }
	    //[COD_EMPLEADO] [bigint] NULL,
        private long codEmpleado = 0;

        public virtual long CodEmpleado
        {
            get { return codEmpleado; }
            set { codEmpleado = value; }
        }

        private double tarjetas = 0;

        public virtual double Tarjetas
        {
            get { return tarjetas; }
            set { tarjetas = value; }
        }

        private double tarjetaPlus = 0;

        public virtual double TarjetaPlus
        {
            get { return tarjetaPlus; }
            set { tarjetaPlus = value; }
        }
    }
}
