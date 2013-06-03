using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.VO;
using EstacionDB.Exceptions;
using NHibernate;
using System.Collections;
using EstacionDB.Helper;

namespace EstacionDB.DAO
{
    public class TanquesDAO
    {
        public List<TanqueVO> consultarTanques() 
        {
            List<TanqueVO> tanques = new List<TanqueVO>();
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(TanqueVO));

                IList tmp = criteria.List();

                foreach (TanqueVO t in tmp)
                {
                    tanques.Add(t);
                }

                ConnectionHelper.CloseSession();

                return tanques;
            } catch(Exception ex){
                throw new EstacionDBException("Error al leer la tabla tanques", ex);
            }
        }

        public int guardarTanque(TanqueVO t) 
        {
            int rows = 0;
            ITransaction tx = null;

            try
            {
                ISession session = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo);
                tx = session.BeginTransaction();

                if (t.IdTanque != null && t.IdTanque != 0)
                {
                    session.Save(t);
                }
                else
                {
                    session.Update(t);
                }

                tx.Commit();
                rows++;

                ConnectionHelper.CloseSession();

                return rows;

            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error al guardar en la tabla tanques", ex);
            }
        }
    }
}
