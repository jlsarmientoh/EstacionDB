using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using EstacionDB.VO;
using EstacionDB.Helper;
using EstacionDB.Exceptions;
using NHibernate;
using NHibernate.Criterion;
using System.Collections;

namespace EstacionDB.DAO
{
    public class IngresosDAO
    {

        private SqlConnection con;

        public List<IngresoVO> consultarIngresosFecha(DateTime fecha1, DateTime fecha2)
        {
            List<IngresoVO> ingresos = new List<IngresoVO>();
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(IngresoVO))
                    .Add(Expression.Between("Fecha", fecha1, fecha2));

                IList tmp = criteria.List();

                foreach (IngresoVO ingreso in tmp)
                {
                    ingresos.Add(ingreso);
                }

                ConnectionHelper.CloseSession();

                return ingresos;

            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la vista ingresos.", ex);                
            }
        }

        public int guardarIngresos(List<IngresoVO> ingresos, DateTime fecha)
        {
            int rows = 0;
            ITransaction tx = null;
            try
            {
                ISession session = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo);
                tx = session.BeginTransaction();

                foreach (IngresoVO ingreso in ingresos)
                {
                    ingreso.Fecha = fecha;
                    session.Save(ingreso);
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
                throw new EstacionDBException("Error al leer la información de la tabla Ingresos.", ex);
            }
        }

        public double consultarTotalIngresosFecha(long codEmpleado, long turno, string isla, DateTime fecha1, DateTime fecha2)
        {
            double total = 0;
            try
            {
                #region  se abre la conexión con la BD
                conectar(Utilidades.Utilidades.appCadenaConexion);
                #endregion

                #region se preparan los objetos para hacer la consulta y leerla
                SqlDataReader reader = null;
                SqlCommand query = new SqlCommand("SELECT SUM(VALOR) AS TOTAL FROM INGRESOS WHERE FECHA BETWEEN '" + fecha1.ToString("dd-MM-yyyy") + "' AND '" + fecha2.ToString("dd-MM-yyyy") + "'", con);
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
            catch (System.Exception ex)
            {
                desconectar();
                throw new EstacionDBException("Error al leer la información de la tabla ingresos.", ex);
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
                    con.Open();
                }
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
