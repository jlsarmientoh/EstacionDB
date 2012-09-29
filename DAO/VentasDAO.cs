using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using EstacionDB.Helper;
using EstacionDB.VO;
using EstacionDB.Exceptions;
using System.Collections;
using NHibernate;
using NHibernate.Criterion;

namespace EstacionDB.DAO
{
    public class VentasDAO
    {
        private SqlConnection con;

        public List<VentaVO> consultarVentasFidelizados(long codEmp, DateTime fecha1, DateTime fecha2, int[] isla, int turno)
        {
            List<VentaVO> ventas = new List<VentaVO>();
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configServ).CreateCriteria(typeof(VentaVO))
                    .Add(Expression.Not(Expression.Eq("Cliente", "CLIENTE NO FIDELIZADO")))
                    .Add(Expression.Between("Fecha", fecha1, fecha2))
                    .Add(Expression.Eq("Turno", turno))
                    .Add(Expression.In("Isla", isla))
                    .Add(Expression.Eq("CodEmpleado", codEmp));
                IList tmp = criteria.List();

                foreach (VentaVO venta in tmp)
                {
                    venta.ModoPago = 7;
                    if (venta.Isla == 1 || venta.Isla == 2)
                    {
                        venta.Isla = 1;
                    }
                    else if (venta.Isla == 3 || venta.Isla == 4)
                    {
                        venta.Isla = 2;
                    }
                    ventas.Add(venta);
                }
                
                ConnectionHelper.CloseSession();
                
                return ventas;            
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la vista Ventas.",ex);                
            }
        }

        public List<VentaVO> consultarVentasNoFidelizados(long codEmp,DateTime fecha1, DateTime fecha2, int[] isla, int turno)
        {
            List<VentaVO> ventas = new List<VentaVO>();
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configServ).CreateCriteria(typeof(VentaVO))
                    .Add(Expression.Eq("Cliente", "CLIENTE NO FIDELIZADO"))
                    .Add(Expression.Between("Fecha", fecha1, fecha2))
                    .Add(Expression.Eq("Turno", turno))
                    .Add(Expression.In("Isla", isla))
                    .Add(Expression.Eq("CodEmpleado", codEmp));
                IList tmp = criteria.List();

                foreach (VentaVO venta in tmp)
                {
                    venta.ModoPago = 0;
                    if (venta.Isla == 1 || venta.Isla == 2)
                    {
                        venta.Isla = 1;
                    }
                    else if (venta.Isla == 3 || venta.Isla == 4)
                    {
                        venta.Isla = 2;
                    }
                    ventas.Add(venta);
                }

                ConnectionHelper.CloseSession();

                return ventas;                
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la vista Ventas.", ex);
                
            }
        }

        public List<VentaTurnoVO> consultarVentasTurno(long codEmp, DateTime fecha1, DateTime fecha2, int[] isla, int turno)
        {
            List<VentaTurnoVO> ventas = new List<VentaTurnoVO>();
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configServ).CreateCriteria(typeof(VentaVO))                    
                    .Add(Expression.Between("Fecha", fecha1, fecha2))
                    .Add(Expression.Eq("Turno", turno))
                    .Add(Expression.In("Isla", isla))
                    .Add(Expression.Eq("CodEmpleado", codEmp));
                IList tmp = criteria.List();

                foreach (VentaVO venta in tmp)
                {
                    VentaTurnoVO v = new VentaTurnoVO();
                    v.CodEmpleado = venta.CodEmpleado;
                    v.Fecha = venta.Fecha;
                    if (venta.Isla == 1 || venta.Isla == 2)
                    {
                        v.Isla = 1;
                    }
                    else if (venta.Isla == 3 || venta.Isla == 4)
                    {
                        v.Isla = 2;
                    }
                    v.Producto = venta.Producto;
                    v.Tiquete = venta.Tiquete;
                    v.Total = venta.Total;
                    v.Turno = venta.Turno;
                    v.Galones = venta.Galones;
                    ventas.Add(v);
                }

                ConnectionHelper.CloseSession();

                return ventas;
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la vista Ventas.", ex);

            }
        }

        public VentaVO consultarVentasByTiquete(long nroTiquete)
        {
            VentaVO tmpVenta = null;
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configServ).CreateCriteria(typeof(VentaVO))
                    .Add(Expression.Eq("Tiquete", nroTiquete));
                
                tmpVenta = criteria.UniqueResult<VentaVO>();

                if (tmpVenta.Isla == 1 || tmpVenta.Isla == 2)
                {
                    tmpVenta.Isla = 1;
                }
                else if (tmpVenta.Isla == 3 || tmpVenta.Isla == 4)
                {
                    tmpVenta.Isla = 2;
                }
                
                ConnectionHelper.CloseSession();

                return tmpVenta;
            }
            catch (System.Exception ex)
            {   
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la vista Ventas.", ex);                
            }
        }

        public VentaVO consultarVentasByTiqueteTurno(long nroTiquete, DateTime fecha1, DateTime fecha2, int[] isla, int turno)
        {
            VentaVO tmpVenta = null;
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configServ).CreateCriteria(typeof(VentaVO))
                    .Add(Expression.Eq("Tiquete", nroTiquete))
                    .Add(Expression.Between("Fecha", fecha1, fecha2))
                    .Add(Expression.Eq("Turno", turno))
                    .Add(Expression.In("Isla", isla));

                tmpVenta = criteria.UniqueResult<VentaVO>();

                if (tmpVenta.Isla == 1 || tmpVenta.Isla == 2)
                {
                    tmpVenta.Isla = 1;
                }
                else if (tmpVenta.Isla == 3 || tmpVenta.Isla == 4)
                {
                    tmpVenta.Isla = 2;
                }

                ConnectionHelper.CloseSession();

                return tmpVenta;
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la vista Ventas.", ex);
            }
        }

        public double consultarTotalVentasFidelizados(long codEmpleado, long turno, string isla, DateTime fecha1, DateTime fecha2)
        {
            double total= 0;
            try
            {
                #region  se abre la conexión con la BD
                conectar(null);
                #endregion

                #region se preparan los objetos para hacer la consulta y leerla
                SqlDataReader reader = null;
                SqlCommand query = new SqlCommand("SELECT SUM(TOTAL) AS TOTAL FROM " + Utilidades.Utilidades.nombreVistaVentas + " WHERE FECHA BETWEEN '" + fecha1.ToString("dd-MM-yyyy") + "' AND '" + fecha2.ToString("dd-MM-yyyy") + "' AND [Isla] in(" + isla + ") AND [Turno] = " + turno + " AND [COD_EMP] = " + codEmpleado + " AND Cliente <> 'CLIENTE NO FIDELIZADO'", con);
                #endregion

                #region se ejecuta el query, se lee el resultado y se procesa en el VO;
                reader = query.ExecuteReader();
                if (reader != null)
                {
                    // Si tiene reaultados los recorre fila por fila
                    while (reader.Read())
                    {
                        if (reader["TOTAL"] != null) total = double.Parse(reader["TOTAL"].ToString());
                    }
                }
                #endregion

                desconectar();
                return total;
            }
            catch(System.FormatException fe){
                desconectar();
                return total;
            }
            catch (System.Exception ex)
            {
                desconectar();
                throw new EstacionDBException("Error al leer la información de la vista Ventas.", ex);
            }
        }

        public int guardarVentas(List<VentaVO> ventas)
        {
            int rows = 0;            
            ITransaction tx = null;
            try
            {
                ISession session = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo);
                tx = session.BeginTransaction();

                DateTime fechaRegistro = DateTime.Now;

                foreach (VentaVO tmp in ventas)
                {
                    tmp.FechaRegistro = fechaRegistro;
                    session.Save(tmp);
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
                throw new EstacionDBException("Error al leer la información de la tabla ventas.", ex);
            }
        }

        public int guardarVentasTurno(List<VentaTurnoVO> ventas)
        {
            int rows = 0;
            ITransaction tx = null;
            try
            {
                ISession session = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo);
                tx = session.BeginTransaction();

                DateTime fechaRegistro = DateTime.Now;

                foreach (VentaTurnoVO tmp in ventas)
                {   
                    session.Save(tmp);
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
                throw new EstacionDBException("Error al leer la información de la tabla ventas.", ex);
            }
        }

        public List<VentaVO> consultarVentasAgrupadas(DateTime fecha1, DateTime fecha2)
        {
            List<VentaVO> ventas = new List<VentaVO>();
            try
            {
                string sqlQuery = "Select v.Fecha, v.Nit, sum(v.Total) From EstacionDB.VO.VentaVO v Where Fecha Between :Fecha1 And :Fecha2 Group By v.Fecha, v.Nit";
                IQuery query = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateQuery(sqlQuery);
                query.SetParameter("Fecha1", fecha1);
                query.SetParameter("Fecha2", fecha2);
                IList tmp = query.List();
                
                foreach (object[] venta in tmp)
                {
                   VentaVO v = new VentaVO();
                   v.Fecha = DateTime.Parse(venta[0].ToString());
                   v.Nit = venta[1].ToString();
                   v.Total = double.Parse(venta[2].ToString());
                   ventas.Add(v);                
                }

                ConnectionHelper.CloseSession();

                return ventas;
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la vista Ventas.", ex);

            }
        }

        private void conectar(string conectionString)
        {
            try
            {
                if (con == null)
                {
                    if (conectionString != null)
                    {
                        con = ConnectionHelper.createConecction(conectionString);
                    }
                    else
                    {
                        con = ConnectionHelper.createDafaultConnection();
                    }
                }
                con.Open();
            }
            catch (System.Exception e)
            {
                throw new EstacionDBException("Ha ocurrido un error al abrir la conexión con la base de datos",e);
            }
        }

        private void desconectar()
        {
            try
            {
                if (con != null)
                {
                    con.Close(); 
                }
            }
            catch (System.Exception e)
            {
                throw new EstacionDBException("Ha ocurrido un error al cerrar la conexión con la base de datos", e);
            }
        }
    }
}
