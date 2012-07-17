using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.VO;
using System.Data.SqlClient;
using EstacionDB.Helper;
using EstacionDB.Exceptions;
using NHibernate;
using NHibernate.Criterion;
using System.Collections;

namespace EstacionDB.DAO
{
    public class EgresosDAO
    {
        
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
                throw new EstacionDBException("Error al leer la información de la tabla Egresos.", ex);
            }
        }
    }
}
