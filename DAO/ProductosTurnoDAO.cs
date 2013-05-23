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

        public List<ProductoTurnoVO> consultarProductosTurno(string isla, long turno, DateTime fecha1, DateTime fecha2)
        {
            List<ProductoTurnoVO> productosTurno = new List<ProductoTurnoVO>();
            int multiplicador = 1;
            try
            {
                #region  se abre la conexión con la BD
                conectar(null);
                #endregion

                #region se preparan los objetos para hacer la consulta y leerla
                SqlDataReader reader = null;
                SqlCommand query = new SqlCommand("SELECT P.Fecha, P.Isla, P.Turno, P.Galones, P.Valor, P.Producto FROM ViewProductosTurno P " +
                        "WHERE P.Fecha BETWEEN '" + fecha1.ToString("dd-MM-yyyy") + "' AND '" + fecha2.ToString("dd-MM-yyyy") + "' AND Isla IN(" + isla + ") AND Turno = " + turno, con);                
                #endregion

                #region se ejecuta el query, se lee el resultado y se procesa en el VO;
                reader = query.ExecuteReader();
                if (reader != null)
                {
                    // Si tiene reaultados los recorre fila por fila
                    while (reader.Read())
                    {
                        multiplicador = 1; // default
                        ProductoTurnoVO pt = new ProductoTurnoVO();
                        if (reader["Fecha"] != null) pt.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                        if (reader["Turno"] != null) pt.Turno = long.Parse(reader["Turno"].ToString());
                        if (reader["Isla"] != null) pt.Isla = long.Parse(reader["Isla"].ToString());
                        if (reader["Producto"] != null) pt.Producto = reader["Producto"].ToString();
                        if (reader["Valor"] != null) pt.Valor = double.Parse(reader["Valor"].ToString());
                        if (reader["Galones"] != null) pt.Galones = double.Parse(reader["Galones"].ToString());
                        /*if (pt.Isla == 1 || pt.Isla == 2)
                        {
                            pt.Isla = 1;
                        }
                        else if (pt.Isla == 3 || pt.Isla == 4)
                        {
                            pt.Isla = 2;
                        }*/
                        if (pt.Producto.Trim().Equals("CORRIENTE"))
                        {
                            multiplicador = Utilidades.Utilidades.corrienteMultiplicarX;
                        }
                        if (pt.Producto.Trim().Equals("SUPER"))
                        {
                            multiplicador = Utilidades.Utilidades.superMultiplicarX;
                        }
                        if (pt.Producto.Trim().Equals("DIESEL"))
                        {
                            multiplicador = Utilidades.Utilidades.dieselMultiplicarX;
                        }
                        pt.Valor = (pt.Valor * multiplicador);
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
                throw new EstacionDBException("Error al leer la información de la vista Ventas.", ex);
            }
        }

        public List<ProductoTurnoVO> consultarProductosAgrupados(DateTime fecha1, DateTime fecha2)
        {
            List<ProductoTurnoVO> productos = new List<ProductoTurnoVO>();
            try
            {
                string sqlQuery = "Select v.Fecha, v.Producto, sum(v.Galones), sum(v.Valor) From EstacionDB.VO.ProductoTurnoVO v Where Fecha Between :Fecha1 And :Fecha2 Group By v.Fecha, v.Producto";
                IQuery query = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateQuery(sqlQuery);
                query.SetParameter("Fecha1", fecha1);
                query.SetParameter("Fecha2", fecha2);
                IList tmp = query.List();

                foreach (object[] producto in tmp)
                {
                    ProductoTurnoVO p = new ProductoTurnoVO();
                    p.Fecha = DateTime.Parse(producto[0].ToString());
                    p.Producto = producto[1].ToString();
                    p.Galones = double.Parse(producto[2].ToString());
                    p.Valor = double.Parse(producto[3].ToString());
                    productos.Add(p);
                }

                ConnectionHelper.CloseSession();

                return productos;
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la tabla Productos_turno Expo.", ex);

            }
        }

        

        public double consultarTotalConsumo(DateTime fecha1, DateTime fecha2)
        {
            double total = 0;
            try
            {
                string sqlQuery = "Select sum(v.Valor) From EstacionDB.VO.ProductoTurnoVO v Where Fecha Between :Fecha1 And :Fecha2";
                IQuery query = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateQuery(sqlQuery);
                query.SetParameter("Fecha1", fecha1);
                query.SetParameter("Fecha2", fecha2);
                object tmp = query.UniqueResult();

                total += double.Parse(tmp.ToString());

                ConnectionHelper.CloseSession();

                return total;
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la tabla Productos_turno Expo.", ex);

            }
        }

        public double consultarTotalGalones(DateTime fecha1, DateTime fecha2)
        {
            double total = 0;
            try
            {
                string sqlQuery = "Select sum(v.Galones) From EstacionDB.VO.ProductoTurnoVO v Where Fecha Between :Fecha1 And :Fecha2";
                IQuery query = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateQuery(sqlQuery);
                query.SetParameter("Fecha1", fecha1);
                query.SetParameter("Fecha2", fecha2);
                object tmp = query.UniqueResult();

                total += double.Parse(tmp.ToString());

                ConnectionHelper.CloseSession();

                return total;
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la tabla Productos_turno Expo.", ex);

            }
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
                throw new EstacionDBException("Error al guardar la información de la Productos turno Expo.", ex);
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
                throw new EstacionDBException("Ha ocurrido un error al abrir la conexión con la base de datos", e);
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
