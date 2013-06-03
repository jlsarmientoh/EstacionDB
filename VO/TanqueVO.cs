using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class TanqueVO
    {
        private int idTanque;

        public virtual int IdTanque
        {
            get { return idTanque; }
            set { idTanque = value; }
        }
        private string descripcion;

        public virtual string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

    }
}
