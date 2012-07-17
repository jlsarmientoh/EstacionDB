using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class EmpleadoVO
    {
        private long id;

        public virtual long Id
        {
            get { return id; }
            set { id = value; }
        }
        private string nombre;

        public virtual string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
    }
}
