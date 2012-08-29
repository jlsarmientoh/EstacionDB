using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using EstacionDB.Helper;
using EstacionDB.Exceptions;
using EstacionDB.VO;
using NHibernate.Criterion;
using System.Collections;
using System.Data.SqlClient;

namespace EstacionDB.DAO
{
    public class ProductosTurnoDAO
    {
        private SqlConnection con;

        public List<ProductoTurnoVO> consultarProductosTurno(int[] isla, long turno, DateTime fecha1, DateTime fecha2)
        {
            List<ProductoTurnoVO> productosTurno = new List<ProductoTurnoVO>();
            try
            {
                #region  se abre la conexi�n con la BD
                conectar(null);
                #endregion

                #region se preparan los objetos para hacer la consulta y leerla
                SqlDataReader reader = null;
                SqlCommand query = new SqlCommand("SELECT P.Fecha, P.Isla, P.Turno, P.Galones, P.Valor, P.Producto FROM ViewProductosTurno P " +
                        "WHERE P.Fecha BETWEEN '" + fecha1.ToString("dd-MM-yyyy") + "' AND '" + fecha2.ToString("dd-MM-yyyy") + "' AND Isla IN(" + isla[0] + "," + isla[1] + ") AND Turno = " + turno, con);                
                #endregion

                #region se ejecuta el query, se lee el resultado y se procesa en el VO;
                reader = query.ExecuteReader();
                if (reader != null)
                {
                    // Si tiene reaultados los recorre fila por fila
                    while (reader.Read())
                    {
                        ProductoTurnoVO pt = new ProductoTurnoVO();
                        if (reader["Fecha"] != null) pt.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                        if (reader["Turno"] != null) pt.Turno = long.Parse(reader["Turno"].ToString());
                        if (reader["Isla"] != null) pt.Isla = long.Parse(reader["Isla"].ToString());
                        if (reader["Producto"] != null) pt.Producto = reader["Producto"].ToString();
                        if (reader["Valor"] != null) pt.Valor = double.Parse(reader["Valor"].ToString());
                        if (reader["Galones"] != null) pt.Galones = double.Parse(reader["Galones"].ToString());
                        if (pt.Isla == 1 || pt.Isla == 2)
                        {
                            pt.Isla = 1;
                        }
                        else if (pt.Isla == 3 || pt.Isla == 4)
                        {
                            pt.Isla = 2;
                        }
                        pt.Valor = (pt.Valor * Utilidades.Utilidades.multiplicarX);
                        productosTurno.Add(pt);
                    }
                }
                #endregion

                desconectar();
                return productosTurno;
            }            
            catch (System.Exception ex)
            {
                desconectar();
                throw new EstacionDBException("Error al leer la informaci�n de la vista Ventas.", ex);
            }
            /*try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configServ).CreateCriteria(typeof(ProductoTurnoVO))
                    .Add(Expression.Between("Fecha", fecha1, fecha2))
                    .Add(Expression.In("Isla", isla))
                    .Add(Expression.Eq("Turno", turno))
                    .AddOrder(Order.Asc("Fecha"))
                    .AddOrder(Order.Asc("Isla"))
                    .AddOrder(Order.Asc("Turno"))
                    .AddOrder(Order.Asc("Producto"));

                IQuery query = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configServ).
                    CreateSQLQuery("SELECT P.Fecha, P.Isla, P.Turno, P.Galones, P.Valor, P.Producto FROM ViewProductosTurno P " +
                        "WHERE P.Fecha BETWEEN :Fecha1 AND :Fecha2 AND Isla IN(:isla) AND Turno = :Turno");
                query.SetParameter("Fecha1", fecha1);
                query.SetParameter("Fecha2", fecha2);
                query.SetParameter("isla", isla);
                query.SetParameter("Turno", turno);                

                //IList tmp = criteria.List();
                System.Collections.Generic.IList<ProductoTurnoVO> tmp = query.List<ProductoTurnoVO>();

                foreach (ProductoTurnoVO productoTurno in tmp)
                {
                    ProductoTurnoVO pt = new ProductoTurnoVO();
                    pt = productoTurno;
                    if (pt.Isla == 1 || pt.Isla == 2)
                    {
                        pt.Isla = 1;
                    }
                    else if (pt.Isla == 3 || pt.Isla == 4)
                    {
                        pt.Isla = 2;
                    }
                    pt.Valor = (pt.Valor * Utilidades.Utilidades.multiplicarX);
                    productosTurno.Add(pt);
                }

                ConnectionHelper.CloseSession();

                return productosTurno;
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la informaci�n de la vista Productos Turno.", ex);
            }
             */ 
        }

        public int guardarProductosTurno(List<ProductoTurnoVO> productosTurno)
        {
            int rows = 0;
            ITransaction tx = null;

            try
            {
                ISession session = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo);
                tx = session.BeginTransaction();

                foreach (ProductoTurnoVO tmp in productosTurno)
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
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al guardar la informaci�n de la Productos turno Expo.", ex);
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
                throw new EstacionDBException("Ha ocurrido un error al abrir la conexi�n con la base de datos", e);
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
                throw new EstacionDBException("Ha ocurrido un error al cerrar la conexi�n con la base de datos", e);
            }
        }
    }
}