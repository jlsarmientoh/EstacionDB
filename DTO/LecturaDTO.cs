using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.DTO
{
    public class LecturaDTO
    {
        private double nivel;

        public double Nivel
        {
            get { return nivel; }
            set { nivel = value; }
        }
        private double galones;

        public double Galones
        {
            get { return galones; }
            set { galones = value; }
        }
        private int idProducto;

        public int IdProducto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }
        private string producto;

        public string Producto
        {
            get { return producto; }
            set { producto = value; }
        }
        private int idTanque;

        public int IdTanque
        {
            get { return idTanque; }
            set { idTanque = value; }
        }
        private string tanque;

        public string Tanque
        {
            get { return tanque; }
            set { tanque = value; }
        }
    }
}
