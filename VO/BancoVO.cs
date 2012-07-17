using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class BancoVO
    {
        private long idBanco;

        public long IdBanco
        {
            get { return idBanco; }
            set { idBanco = value; }
        }
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
    }
}
