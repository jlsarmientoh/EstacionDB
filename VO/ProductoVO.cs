using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class ProductoVO
    {
        //[ID_PRODUCTO] [int] IDENTITY(1,1) NOT NULL,
        private int idProducto;

        public virtual int IdProducto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }
	    //[NOMBRE] [varchar](15) NOT NULL,
        private string nombre;

        public virtual string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
    }
}
