using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.VO;
using EstacionDB.Exceptions;
using NHibernate;
using EstacionDB.Helper;
using NHibernate.Criterion;
using System.Collections;

namespace EstacionDB.DAO
{
    public class VolumenTanqueDAO
    {
        public List<VolumenTanqueVO> consultarVolumenes()
        {
            List<VolumenTanqueVO> volumenes = new List<VolumenTanqueVO>();
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(VolumenTanqueVO));
                    
                IList tmp = criteria.List();

                foreach (VolumenTanqueVO vt in tmp)
                {
                    volumenes.Add(vt);
                }

                ConnectionHelper.CloseSession();

                return volumenes;
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error al consultar la información de la tabla volumenes tanque", ex);
            }
        }

        public List<VolumenTanqueVO> consultarVolumenes(int idTanque, int idProducto)
        {
            List<VolumenTanqueVO> volumenes = null;
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(VolumenTanqueVO))                    
                    .Add(Expression.Eq("idTanque", idTanque))
                    .Add(Expression.Eq("idProducto", idProducto));

                IList tmp = criteria.List();

                foreach (VolumenTanqueVO vt in tmp)
                {
                    volumenes.Add(vt);
                }

                ConnectionHelper.CloseSession();

                return volumenes;
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error al consultar la información de la tabla volumenes tanque", ex);
            }
        }

        public VolumenTanqueVO consultarVolumen(int idTanque, int idProducto, double cms)
        {
            VolumenTanqueVO volumen = null;
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(VolumenTanqueVO))
                    .Add(Expression.Eq("idTanque", idTanque))
                    .Add(Expression.Eq("idProducto", idProducto))
                    .Add(Expression.Eq("Cms", cms));

                volumen = (VolumenTanqueVO)criteria.UniqueResult();

                ConnectionHelper.CloseSession();

                return volumen;
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error al consultar la información de la tabla volumenes tanque", ex);
            }
        }

        public int guardarVolumenTanque(VolumenTanqueVO volumen)
        {
            int rows = 0;
            ITransaction tx = null;

            try
            {
                ISession session = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo);
                tx = session.BeginTransaction();

                if (volumen.IdVolumen != null && volumen.IdVolumen != 0)
                {
                    session.Save(volumen);
                }
                else
                {
                    session.Update(volumen);
                }

                tx.Commit();
                rows++;

                ConnectionHelper.CloseSession();

                return rows;

            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error al guardar la información en la tabla volumenes tanque", ex);
            }
        }
    }
}
