using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class UsuarioVO
    {
        private string idUsuario;

        public virtual string IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }
        private string pwd;

        public virtual string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }
        private bool isAdmin;

        public virtual bool IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; }
        }
    }
}
