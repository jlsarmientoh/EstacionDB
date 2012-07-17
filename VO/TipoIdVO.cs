using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class TipoIdVO
    {
        public TipoIdVO(long tipo, string nom)
        {
            idTipo = tipo;
            nombre = nom;
        }

        public TipoIdVO()
        {
        }

        private long idTipo;

        public long IdTipo
        {
            get { return idTipo; }
            set { idTipo = value; }
        }
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }


    }
}
