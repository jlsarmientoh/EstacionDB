using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class LecturaVO
    {
        private DateTime fecha; //[Fecha]

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        private double lecturaFinal; //[Lec_Final]

        public double LecturaFinal
        {
            get { return lecturaFinal; }
            set { lecturaFinal = value; }
        }
        private int manguera; //[Manguera]

        public int Manguera
        {
            get { return manguera; }
            set { manguera = value; }
        }
        private int isla; //[Isla]

        public int Isla
        {
            get { return isla; }
            set { isla = value; }
        }
        private int turno; //[Turno]

        public int Turno
        {
            get { return turno; }
            set { turno = value; }
        }
        private string producto; //[Producto]

        public string Producto
        {
            get { return producto; }
            set { producto = value; }
        }
        private double ventaGalones; //[DIFERENCIA]

        public double VentaGalones
        {
            get { return ventaGalones; }
            set { ventaGalones = value; }
        }
        private double precio; //[PRECIO]

        public double Precio
        {
            get { return precio; }
            set { precio = value; }
        }
        private double ventaTotal; //[VR_DIFERENCIA]

        public double VentaTotal
        {
            get { return ventaTotal; }
            set { ventaTotal = value; }
        }
        private long codigoArticulo; //[COD_ART]

        public long CodigoArticulo
        {
            get { return codigoArticulo; }
            set { codigoArticulo = value; }
        }
        private double sobretasa;

        public double Sobretasa
        {
            get { return sobretasa; }
            set { sobretasa = value; }
        }
        private double valorSobretasa;

        public double ValorSobretasa
        {
            get { return valorSobretasa; }
            set { valorSobretasa = value; }
        }
        private double ventaNeta;

        public double VentaNeta
        {
            get { return ventaNeta; }
            set { ventaNeta = value; }
        }
    }
}
