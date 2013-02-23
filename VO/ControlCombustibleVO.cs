using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class ControlCombustibleVO
    {
    //[ID] [bigint] IDENTITY(1,1) NOT NULL,
        private int id;

        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }
	//[FECHA] [datetime] NULL,
        private DateTime fecha;

        public virtual DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
	//[GALONES_COMPRA] [int] NULL,
        private int galonesCompra;

        public virtual int GalonesCompra
        {
            get { return galonesCompra; }
            set { galonesCompra = value; }
        }
	//[INV_INICIAL] [int] NULL,
        private int inventarioInicial;

        public virtual int InventarioInicial
        {
            get { return inventarioInicial; }
            set { inventarioInicial = value; }
        }
	//[INV_FINAL] [int] NULL,
        private int inventarioFinal;

        public virtual int InventarioFinal
        {
            get { return inventarioFinal; }
            set { inventarioFinal = value; }
        }
	//[VENTA_MEDIDA] [int] NULL,
        private int ventaMedida;

        public virtual int VentaMedida
        {
            get { return ventaMedida; }
            set { ventaMedida = value; }
        }
	//[VENTA_SURTIDOR] [int] NULL,
        private int ventaSurtidor;

        public virtual int VentaSurtidor
        {
            get { return ventaSurtidor; }
            set { ventaSurtidor = value; }
        }
	//[ID_PRODUCTO] [int] NULL,
        private int idProducto;

        public virtual int IdProducto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }
	//[SOBRANTE_DIA] [int] NULL,
        private int sobranteDia;

        public virtual int SobranteDia
        {
            get { return sobranteDia; }
            set { sobranteDia = value; }
        }
	//[SOBRANTE_ACUMULADO] [int] NULL,
        private int sobranteAcumulado;

        public virtual int SobranteAcumulado
        {
            get { return sobranteAcumulado; }
            set { sobranteAcumulado = value; }
        }
	//[PORCENTAJE_EDS] [float] NULL,
        private double procentaje;

        public virtual double Procentaje
        {
            get { return procentaje; }
            set { procentaje = value; }
        }
    }
}
