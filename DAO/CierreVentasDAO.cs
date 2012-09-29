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

        public List<CierreVentasVO> consultarCierresAgrupados(DateTime fecha1, DateTime fecha2)
        {
            List<CierreVentasVO> cierresVenta = new List<CierreVentasVO>();
            try
            {
                string sqlQuery = "Select v.Fecha, sum(v.TotalVentas), sum(v.Creditos), sum(v.Efectivo), sum(v.Sodexo), sum(v.BigPass), sum(v.Otros), sum(v.Tarjetas), sum(v.TarjetaPlus), sum(v.TicketTronik) From EstacionDB.VO.CierreVentasVO v Where Fecha Between :Fecha1 And :Fecha2 Group By v.Fecha";
                IQuery query = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateQuery(sqlQuery);
                query.SetParameter("Fecha1", fecha1);
                query.SetParameter("Fecha2", fecha2);
                IList tmp = query.List();

                foreach (object[] cierre in tmp)
                {
                    CierreVentasVO cv= new CierreVentasVO();
                    cv.Fecha = DateTime.Parse(cierre[0].ToString());
                    cv.TotalVentas = double.Parse(cierre[1].ToString());
                    cv.Creditos = double.Parse(cierre[2].ToString());
                    cv.Efectivo = double.Parse(cierre[3].ToString());
                    cv.Sodexo = double.Parse(cierre[4].ToString());
                    cv.BigPass = double.Parse(cierre[5].ToString());
                    cv.Otros = double.Parse(cierre[6].ToString());
                    cv.Tarjetas = double.Parse(cierre[7].ToString());
                    cv.TarjetaPlus = double.Parse(cierre[8].ToString());
                    cv.TicketTronik = double.Parse(cierre[9].ToString());
                    cierresVenta.Add(cv);
                }

                ConnectionHelper.CloseSession();

                return cierresVenta;
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la vista Ventas.", ex);

            }
        }
    }
}
