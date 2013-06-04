using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.VO;
using NHibernate;
using EstacionDB.Helper;
using EstacionDB.Exceptions;

namespace EstacionDB.DAO
{
    public class CompraCombustibleDAO
    {
        public int guardar(CompraCombustibleVO c) 
        {
            int rows = 0;
            ITransaction tx = null;

            try
            {
                ISession session = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo);
                tx = session.BeginTransaction();

                if (c.IdCompra != null && c.IdCompra != 0)
                {
                    session.Save(c);
                }
                else
                {
                    session.Update(c);
                }

                tx.Commit();
                rows++;

                ConnectionHelper.CloseSession();

                return rows;
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error al guardar la información en la tabla Compras combustible", ex);
            }
        }


    }
}
