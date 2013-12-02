using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using EstacionDB.VO;
using NHibernate;
using EstacionDB.Helper;
using NHibernate.Criterion;
using EstacionDB.Exceptions;

namespace EstacionDB.DAO
{
    public class CierreEfectivoDAO
    {
        public IList consultarCierres(DateTime fecha1, DateTime fecha2)
        {
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(CierreEfectivoVO))
                    .Add(Expression.Between("Fecha", fecha1, fecha2));

                IList tmp = criteria.List();
                ConnectionHelper.CloseSession();

                return tmp;
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de cierre de efectivo.", ex);
            }
        }

        public int guardarCierre(CierreEfectivoVO cierre)
        {
            int rows = 0;
            ITransaction tx = null;

            try
            {
                ISession session = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo);
                tx = session.BeginTransaction();

                if (cierre.IdCierre == 0)
                {
                    session.Save(cierre);
                }
                else
                {
                    session.Update(cierre);
                }

                tx.Commit();
                rows++;

                ConnectionHelper.CloseSession();

                return rows;

            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al guardar la información de la tabla cierre efectivo.", ex);
            }
        }

        public int contarCierres(DateTime fecha1, DateTime fecha2)
        {
            int total = 0;
            try
            {
                string sqlQuery = "Select count(c.IdCierre) From EstacionDB.VO.CierreEfectivoVO c Where c.Fecha Between :Fecha1 And :Fecha2";
                IQuery query = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateQuery(sqlQuery);
                query.SetParameter("Fecha1", fecha1);
                query.SetParameter("Fecha2", fecha2);
                object tmp = query.UniqueResult();

                if (tmp != null)
                {
                    total = int.Parse(tmp.ToString());
                }

                ConnectionHelper.CloseSession();
                return total;
            }
            catch (Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la tabla cierre efectivo", ex);
            }
        }
    }
}
