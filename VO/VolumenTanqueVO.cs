using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class VolumenTanqueVO
    {
        private long idVolumen; //[ID_VOLUMEN]

        public virtual long IdVolumen
        {
            get { return idVolumen; }
            set { idVolumen = value; }
        }
        private int idTanque;//[ID_TANQUE]

        public virtual int IdTanque
        {
            get { return idTanque; }
            set { idTanque = value; }
        }
        private double cms;//[LECTURA_CM]

        public virtual double Cms
        {
            get { return cms; }
            set { cms = value; }
        }
        private double galones;//[VOLUMEN_GAL]

        public virtual double Galones
        {
            get { return galones; }
            set { galones = value; }
        }
        private int idProducto; //[ID_PRODUCTO]

        public virtual int IdProducto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }
    }
}
