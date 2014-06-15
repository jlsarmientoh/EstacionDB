using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class VentaVO
    {
        private DateTime fecha; //[FECHA]

        public virtual DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        private string nit; //[NIT]

        public virtual string Nit
        {
            get { return nit; }
            set { nit = value; }
        }
        private string cliente;//[Cliente]

        public virtual string Cliente
        {
            get { return cliente; }
            set { cliente = value; }
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
        private string placa; //[Vehiculo_Placa]

        public virtual string Placa
        {
            get { return placa; }
            set { placa = value; }
        }
        private double kilometraje; //[KIL_ACT]

        public virtual double Kilometraje
        {
            get { return kilometraje; }
            set { kilometraje = value; }
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

        private long modoPago;

        public virtual long ModoPago
        {
            get { return modoPago; }
            set { modoPago = value; }
        }

        private long codEmpleado;

        public virtual long CodEmpleado
        {
            get { return codEmpleado; }
            set { codEmpleado = value; }
        }

        private DateTime fechaRegistro;

        public virtual DateTime FechaRegistro
        {
            get { return fechaRegistro; }
            set { fechaRegistro = value; }
        }

        private double galones;
        public virtual double Galones
        {
            get { return galones; }
            set { galones = value; }
        }

        private double descuento;

        public virtual double Descuento
        {
            get { return descuento; }
            set { descuento = value; }
        }


    }
}
