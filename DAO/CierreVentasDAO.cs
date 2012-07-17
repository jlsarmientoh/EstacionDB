using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using EstacionDB.Exceptions;
using EstacionDB.Helper;
using EstacionDB.VO;
using NHibernate;
using NHibernate.Criterion;
using System.Collections;

namespace EstacionDB.DAO
{
    public class CierreVentasDAO
    {

        public int consultarCuentaCierres(long codEmpleado, long turno, long isla, DateTime fecha1, DateTime fecha2)
        {
            int total = 0;
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(CierreVentasVO))
                    .Add(Expression.Between("Fecha", fecha1, fecha2))
                    .Add(Expression.Eq("Turno", turno))
                    .Add(Expression.Eq("Isla", isla))
                    .Add(Expression.Eq("CodEmpleado", codEmpleado));

                IList tmp = criteria.List();

                total = tmp.Count;
                /*                
                "SELECT COUNT(ID_CIERRE) AS ID_CIERRE FROM CIERRE_VENTAS WHERE FECHA BETWEEN '" + fecha1.ToString("dd-MM-yyyy") + "' AND '" + fecha2.ToString("dd-MM-yyyy") + "' AND [ISLA] = " + isla + " AND [TURNO] = " + turno + " AND [COD_EMPLEADO] = " + codEmpleado                
                 */
                ConnectionHelper.CloseSession();

                return total;
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la vista Ventas.", ex);
            }
        }

        public int guardarCierre(CierreVentasVO cierre)
        {
            int rows = 0;
            ITransaction tx = null;

            try
            {
                ISession session = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo);
                tx = session.BeginTransaction();

                session.Save(cierre);

                tx.Commit();
                rows++;

                ConnectionHelper.CloseSession();
                
                return rows;

            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la tabla ventas.", ex);
            }
        }

        public IList consultarCierres(DateTime fecha1, DateTime fecha2)
        {
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(CierreVentasVO))
                    .Add(Expression.Between("Fecha", fecha1, fecha2));

                IList tmp = criteria.List();
                ConnectionHelper.CloseSession();

                return tmp;
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la vista Ventas.", ex);
            }
        }
    }
}
