using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.DTO
{
    public class NitDTO
    {
        private string nit;

        public string Nit
        {
            get { return nit; }
            set { nit = value; }
        }
        private string digitoVerfificacion;

        public string DigitoVerfificacion
        {
            get { return digitoVerfificacion; }
            set { digitoVerfificacion = value; }
        }
        private string codigo;

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
    }
}
