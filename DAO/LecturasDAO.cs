using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using EstacionDB.VO;
using EstacionDB.Exceptions;
using EstacionDB.Helper;

namespace EstacionDB.DAO
{
    public class LecturasDAO
    {
        private SqlConnection con;

        public List<LecturaVO> consultarLecturas(DateTime fecha1, DateTime fecha2)
        {
            List<LecturaVO> lecturas = new List<LecturaVO>();
            try
            {
                #region  se abre la conexión con la BD
                conectar();
                #endregion

                #region se preparan los objetos para hacer la consulta y leerla
                SqlDataReader reader = null;
                SqlCommand query = new SqlCommand("SELECT [Fecha],[Lec_Final],[Manguera],[Isla],[Turno],[Producto],[DIFERENCIA],[PRECIO],[VR_DIFERENCIA], [COD_ART] FROM " + Utilidades.Utilidades.nombreVistaLecturas + " WHERE FECHA BETWEEN '" + fecha1.ToString("dd-MM-yyyy") + "' AND '" + fecha2.ToString("dd-MM-yyyy") + "'", con);
                #endregion

                #region se ejecuta el query, se lee el resultado y se procesa en el VO;
                reader = query.ExecuteReader();
                if (reader != null)
                {
                    // Si tiene reaultados los recorre fila por fila
                    while (reader.Read())
                    {
                        LecturaVO tmpLectura = new LecturaVO();
                        if (reader["FECHA"] != null) tmpLectura.Fecha = DateTime.Parse(reader["FECHA"].ToString());
                        if (reader["Lec_Final"] != null) tmpLectura.LecturaFinal = double.Parse(reader["Lec_Final"].ToString());
                        if (reader["Manguera"] != null) tmpLectura.Manguera = int.Parse(reader["Manguera"].ToString());
                        if (reader["Isla"] != null) tmpLectura.Isla = int.Parse(reader["Isla"].ToString());
                        if (reader["Turno"] != null) tmpLectura.Turno = int.Parse(reader["Turno"].ToString());
                        if (reader["Producto"] != null) tmpLectura.Producto = reader["Producto"].ToString();
                        if (reader["DIFERENCIA"] != null) tmpLectura.VentaGalones = double.Parse(reader["DIFERENCIA"].ToString());
                        if (reader["PRECIO"] != null) tmpLectura.Precio = double.Parse(reader["PRECIO"].ToString());
                        if (reader["VR_DIFERENCIA"] != null) tmpLectura.VentaTotal = double.Parse(reader["VR_DIFERENCIA"].ToString());
                        if (reader["COD_ART"] != null) tmpLectura.CodigoArticulo = long.Parse(reader["COD_ART"].ToString());
                        
                        /*if(Utilidades.Utilidades.codigoCorriente == tmpLectura.CodigoArticulo)
                                {
                                    tmpLectura.Sobretasa = 0;
                                    
                                }
                        if(Utilidades.Utilidades.codigoSuper == tmpLectura.CodigoArticulo)
                                {
                                    tmpLectura.Sobretasa = 0;
                                    
                                }
                        if(Utilidades.Utilidades.codigoDiesel == tmpLectura.CodigoArticulo)
                                {
                                    tmpLectura.Sobretasa = 0;
                                    
                                }
                        

                        tmpLectura.ValorSobretasa = tmpLectura.VentaGalones * tmpLectura.Sobretasa;
                        tmpLectura.VentaNeta = tmpLectura.VentaTotal - tmpLectura.ValorSobretasa;
                        */
                        lecturas.Add(tmpLectura);
                    }
                }
                #endregion

                desconectar();
                return lecturas;

            }
            catch (System.Exception ex)
            {
                desconectar();
                throw new EstacionDBException("Error al leer la información de la vista Lecturas.", ex);                
            }
        }

        public double consultarLecturaFinalProducto(DateTime fecha1, DateTime fecha2, int idProducto)
        {
            double dif = 0;
            
            try
            {
                #region  se abre la conexión con la BD
                conectar();
                #endregion

                #region se preparan los objetos para hacer la consulta y leerla
                SqlDataReader reader = null;
                SqlCommand query = new SqlCommand("SELECT SUM([Lec_Final]) AS Lectura FROM " + Utilidades.Utilidades.nombreVistaLecturas + " WHERE FECHA BETWEEN '" + fecha1.ToString("dd-MM-yyyy") + "' AND '" + fecha2.ToString("dd-MM-yyyy") + "' AND [COD_ART] = " + idProducto, con);
                #endregion

                #region se ejecuta el query, se lee el resultado y se procesa en el VO;
                reader = query.ExecuteReader();
                if (reader != null)
                {
                    // Si tiene reaultados los recorre fila por fila
                    while (reader.Read())
                    {   
                        if (reader["Lectura"] != null) dif = double.Parse(reader["Lectura"].ToString());
                    }
                }
                #endregion

                desconectar();
                return dif;

            }
            catch (System.Exception ex)
            {
                desconectar();
                throw new EstacionDBException("Error al leer la información de la vista Lecturas.", ex);
            }
        }

        public double consultarDiferenciaProducto(DateTime fecha1, DateTime fecha2, int idProducto)
        {
            double dif = 0;
            try
            {
                #region  se abre la conexión con la BD
                conectar();
                #endregion

                #region se preparan los objetos para hacer la consulta y leerla
                SqlDataReader reader = null;
                SqlCommand query = new SqlCommand("SELECT SUM([DIFERENCIA]) AS DIFERENCIA FROM " + Utilidades.Utilidades.nombreVistaLecturas + " WHERE FECHA BETWEEN '" + fecha1.ToString("dd-MM-yyyy") + "' AND '" + fecha2.ToString("dd-MM-yyyy") + "' AND [COD_ART] = " + idProducto, con);
                #endregion

                #region se ejecuta el query, se lee el resultado y se procesa en el VO;
                reader = query.ExecuteReader();
                if (reader != null)
                {
                    // Si tiene reaultados los recorre fila por fila
                    while (reader.Read())
                    {   
                        if (reader["DIFERENCIA"] != null) dif = double.Parse(reader["DIFERENCIA"].ToString());
                    }
                }
                #endregion

                desconectar();
                return dif;

            }
            catch (System.Exception ex)
            {
                desconectar();
                throw new EstacionDBException("Error al leer la información de la vista Lecturas.", ex);
            }
        }

        private void conectar()
        {
            try
            {
                if (con == null)
                {
                    con = ConnectionHelper.createDafaultConnection();
                    con.Open();
                }
                else
                {
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
