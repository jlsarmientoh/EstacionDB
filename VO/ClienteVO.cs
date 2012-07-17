using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class ClienteVO
    {
        private long idCliente;

        public long IdCliente
        {
            get { return idCliente; }
            set { idCliente = value; }
        }
        private long tipoId;

        public long TipoId
        {
            get { return tipoId; }
            set { tipoId = value; }
        }
        private string identificacion;

        public string Identificacion
        {
            get { return identificacion; }
            set { identificacion = value; }
        }
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private string direccion;

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }
        private string telefono;

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }
        private string contacto;

        public string Contacto
        {
            get { return contacto; }
            set { contacto = value; }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
    }
}
