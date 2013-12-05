using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.VO;
using NHibernate;
using EstacionDB.Helper;
using System.Collections;
using NHibernate.Criterion;
using EstacionDB.Exceptions;

namespace EstacionDB.DAO
{
    public class ControlCombustibleDAO
    {

        public List<ControlCombustibleVO> consultarControles()
        {
            List<ControlCombustibleVO> controles = new List<ControlCombustibleVO>();
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(ControlCombustibleVO));

                IList tmp = criteria.List();
                foreach (ControlCombustibleVO cc in tmp)
                {
                    controles.Add(cc);
                }
                ConnectionHelper.CloseSession();

                return controles;
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la tabla control combustible.", ex);                
            }
        }

        public List<ControlCombustibleVO> consultarControles(DateTime fecha1, DateTime fecha2)
        {
            List<ControlCombustibleVO> controles = new List<ControlCombustibleVO>();
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(ControlCombustibleVO))
                    .Add(Expression.Between("Fecha", fecha1, fecha2));

                IList tmp = criteria.List();
                foreach (ControlCombustibleVO cc in tmp)
                {
                    controles.Add(cc);
                }
                ConnectionHelper.CloseSession();

                return controles;
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la tabla control combustible.", ex);
            }
        }

        public List<ControlCombustibleVO> consultarControles(int idProducto, DateTime fecha1, DateTime fecha2)
        {
            List<ControlCombustibleVO> controles = new List<ControlCombustibleVO>();
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(ControlCombustibleVO))
                    .Add(Expression.Between("Fecha", fecha1, fecha2))
                    .Add(Expression.Eq("IdProducto", idProducto));

                IList tmp = criteria.List();
                foreach (ControlCombustibleVO cc in tmp)
                {
                    controles.Add(cc);
                }
                ConnectionHelper.CloseSession();

                return controles;
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la tabla control combustible.", ex);
            }
        }

        public ControlCombustibleVO consultarControl(int idProducto, DateTime fecha)
        {
            ControlCombustibleVO control = null;
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(ControlCombustibleVO))
                    .Add(Expression.Between("Fecha", fecha, fecha))
                    .Add(Expression.Eq("IdProducto", idProducto));

                control = criteria.UniqueResult<ControlCombustibleVO>();
                
                ConnectionHelper.CloseSession();

                return control;
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la tabla control combustible.", ex);
            }
        }

        public int guardarControlCombustible(ControlCombustibleVO cc)
        {
            int rows = 0;
            ITransaction tx = null;
            try
            {
                ISession session = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo);
                tx = session.BeginTransaction();
                if (cc.Id == 0)
                {
                    session.Save(cc);
                }
                else
                {
                    session.Update(cc);
                }

                rows++;
                tx.Commit();

                ConnectionHelper.CloseSession();

                return rows;
            }
            catch (System.Exception ex)
            {
                if (tx != null)
                {
                    tx.Rollback();
                }
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al guardar la información de la tabla control combustible.", ex);
            }
        }
    }
}
