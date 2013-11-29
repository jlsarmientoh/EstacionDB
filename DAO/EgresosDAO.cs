using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.VO;
using System.Data.SqlClient;
using EstacionDB.Helper;
using EstacionDB.Exceptions;
using NHibernate.Criterion;
using System.Collections;
using NHibernate;

namespace EstacionDB.DAO
{
    public class EgresosDAO
    {

        public EgresoVO consultarEgreso(long numero)
        {
            EgresoVO egreso;
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(EgresoVO))
                    .Add(Expression.Eq("Numero", numero));

                egreso = criteria.UniqueResult<EgresoVO>();

                ConnectionHelper.CloseSession();

                return egreso;
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la vista egresos.", ex);
            }
        }

        public List<EgresoVO> consultarEgresosFecha(DateTime fecha1, DateTime fecha2)
        {
            List<EgresoVO> egresos = new List<EgresoVO>();
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(EgresoVO))
                    .Add(Expression.Between("Fecha", fecha1, fecha2));

                IList tmp = criteria.List();

                foreach (EgresoVO egreso in tmp)
                {
                    egresos.Add(egreso);
                }

                ConnectionHelper.CloseSession();
                
                return egresos;

            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la vista egresos.", ex);
            }
        }

        public List<EgresoVO> consultarEgresosAplicadosFecha(DateTime fecha1, DateTime fecha2)
        {
            List<EgresoVO> egresos = new List<EgresoVO>();
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(EgresoVO))
                    .Add(Expression.Between("FechaAplica", fecha1, fecha2));                    

                IList tmp = criteria.List();
                
                foreach (EgresoVO egreso in tmp)
                {
                    egresos.Add(egreso);
                }

                ConnectionHelper.CloseSession();

                return egresos;

            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la vista egresos.", ex);
            }
        }

        public IList consultarEgresosAplicados(DateTime fecha1, DateTime fecha2)
        {
            IList egresos = null;
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(EgresoVO))
                    .Add(Expression.Between("FechaAplica", fecha1, fecha2));

                egresos = criteria.List();

                ConnectionHelper.CloseSession();

                return egresos;

            }            
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la tabla egresos.", ex);
            }
        }

        public double consultarTotalEgresosAplicados(DateTime fecha1, DateTime fecha2)
        {
            double totalAplicado = 0;
            try
            {
                string sqlQuery = "Select sum(e.Valor) From EstacionDB.VO.EgresoVO e Where FechaAplica Between :Fecha1 And :Fecha2";
                IQuery query = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateQuery(sqlQuery);
                query.SetParameter("Fecha1", fecha1);
                query.SetParameter("Fecha2", fecha2);
                object tmp = query.UniqueResult();

                if(tmp != null)
                {
                    totalAplicado = Double.Parse(tmp.ToString());
                }

                ConnectionHelper.CloseSession();
                return totalAplicado;
            }
            catch (Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la tabla egresos", ex);
            }
        }

        public int guardarEgresos(List<EgresoVO> egresos, DateTime fecha, DateTime fechaAplica)
        {
            int rows = 0;
            ITransaction tx = null;
            try
            {
                ISession session = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo);
                tx = session.BeginTransaction();

                foreach (EgresoVO egreso in egresos)
                {
                    egreso.Fecha = fecha;
                    egreso.FechaAplica = fechaAplica;
                    egreso.Isla = 0; // Queda en cero porque la isla no tiene importancia
                    session.Save(egreso);
                    rows++;
                }

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
                throw new EstacionDBException("Error al actualizar la información de la tabla Egresos.", ex);
            }
        }

        public int guardarEgresos(IList<EgresoVO> egresos)
        {
            int rows = 0;
            ITransaction tx = null;
            try
            {
                ISession session = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo);
                tx = session.BeginTransaction();

                foreach (EgresoVO egreso in egresos)
                {   
                    session.SaveOrUpdate(egreso);
                    rows++;
                }

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
                throw new EstacionDBException("Error al actualizar la información de la tabla Egresos.", ex);
            }
        }

        public int eliminarEgreso(EgresoVO egreso)
        {
            int rows = 0;
            ITransaction tx = null;
            try
            {
                ISession session = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo);
                tx = session.BeginTransaction();

                session.Delete(egreso);
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
                throw new EstacionDBException("Error al eliminar el egreso de la tabla Egresos.", ex);
            }
        }
    }
}
