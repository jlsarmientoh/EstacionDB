using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class ClienteVO
    {
        private long idCliente;

        public virtual long IdCliente
        {
            get { return idCliente; }
            set { idCliente = value; }
        }
        private long tipoId;

        public virtual long TipoId
        {
            get { return tipoId; }
            set { tipoId = value; }
        }
        private string identificacion;

        public virtual string Identificacion
        {
            get { return identificacion; }
            set { identificacion = value; }
        }
        private string nombre;

        public virtual string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private string direccion;

        public virtual string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }
        private string telefono;

        public virtual string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }
        private string contacto;

        public virtual string Contacto
        {
            get { return contacto; }
            set { contacto = value; }
        }
        private string email;

        public virtual string Email
        {
            get { return email; }
            set { email = value; }
        }
    }
}