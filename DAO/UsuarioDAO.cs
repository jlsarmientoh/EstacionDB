using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.VO;
using EstacionDB.Exceptions;
using NHibernate;
using System.Collections;
using EstacionDB.Helper;
using NHibernate.Criterion;

namespace EstacionDB.DAO
{
    public class UsuarioDAO
    {
        public List<UsuarioVO> consultarUsuarios()
        {
            List<UsuarioVO> usuarios = new List<UsuarioVO>();

            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(UsuarioVO));

                IList tmp = criteria.List();
                foreach (UsuarioVO usuario in tmp)
                {
                    usuarios.Add(usuario);
                }
                ConnectionHelper.CloseSession();

                return usuarios;
            }
            catch (Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al consultar la tabla usuarios", ex);
            }
        }

        public UsuarioVO consultarUsuario(string idUsuario)
        {
            UsuarioVO usuario = null;

            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(UsuarioVO))
                    .Add(Expression.Eq("IdUsuario", idUsuario));

                usuario = criteria.UniqueResult<UsuarioVO>();
                
                ConnectionHelper.CloseSession();

                return usuario;
            }
            catch (Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al consultar la tabla usuarios", ex);
            }
        }

        public UsuarioVO consultarUsuario(string idUsuario, string pwd)
        {
            UsuarioVO usuario = null;

            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(UsuarioVO))
                    .Add(Expression.Eq("IdUsuario", idUsuario))
                    .Add(Expression.Eq("Pwd", pwd));

                usuario = criteria.UniqueResult<UsuarioVO>();

                ConnectionHelper.CloseSession();

                return usuario;
            }
            catch (Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al consultar la tabla usuarios", ex);
            }
        }

        public int guardarUsuario(UsuarioVO usuario)
        {
            int rows = 0;
            ITransaction tx = null;
            try
            {
                ISession session = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo);
                tx = session.BeginTransaction();
                
                session.SaveOrUpdate(usuario);

                rows++;
                tx.Commit();

                ConnectionHelper.CloseSession();

                return rows;
            }
            catch (Exception ex)
            {
                if (tx != null)
                {
                    tx.Rollback();
                }
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al guardar la información la tabla usuarios.", ex);
            }
        }
    }
}
