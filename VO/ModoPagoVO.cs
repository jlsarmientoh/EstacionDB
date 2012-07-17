using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class ModoPagoVO
    {
        public ModoPagoVO(long id, string desc)
        {
            idModo = id;
            nombre = desc;
        }

        private long idModo;

        public long IdModo
        {
            get { return idModo; }
            set { idModo = value; }
        }
        private String nombre;

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
    }
}
