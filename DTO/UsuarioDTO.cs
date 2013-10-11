using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.DTO
{
    public class UsuarioDTO
    {
        private string usuario;

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }
        private bool isAdmin;

        public bool IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; }
        }

        private string pwd;

        public virtual string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }

        public UsuarioDTO() { }

        public UsuarioDTO(String usr, bool admin)
        {
            this.usuario = usr;
            this.isAdmin = admin;
        }
    }
}
