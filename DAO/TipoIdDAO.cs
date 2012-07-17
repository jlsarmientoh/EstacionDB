using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using EstacionDB.VO;
using EstacionDB.Helper;
using EstacionDB.Exceptions;

namespace EstacionDB.DAO
{
    public class TipoIdDAO
    {
        private SqlConnection con;

        public List<TipoIdVO> consultarTipos()
        {
            List<TipoIdVO> tipos = new List<TipoIdVO>();
            try
            {
                #region  se abre la conexión con la BD
                conectar();
                #endregion

                #region se preparan los objetos para hacer la consulta y leerla
                SqlDataReader reader = null;
                SqlCommand query = new SqlCommand("SELECT [ID_TIPO],[NOMBRE] FROM [TIPO_IDENTIFICACION]", con);
                #endregion

                #region se ejecuta el query, se lee el resultado y se procesa en el VO;
                reader = query.ExecuteReader();
                if (reader != null)
                {
                    // Si tiene reaultados los recorre fila por fila
                    while (reader.Read())
                    {
                        TipoIdVO tmpTipo = new TipoIdVO();
                        if (reader["ID_TIPO"] != null) tmpTipo.IdTipo = long.Parse(reader["ID_TIPO"].ToString());
                        if (reader["NOMBRE"] != null) tmpTipo.Nombre = reader["NOMBRE"].ToString();

                        tipos.Add(tmpTipo);
                    }
                }
                #endregion

                desconectar();
                return tipos;

            }
            catch (System.Exception ex)
            {
                desconectar();
                throw new EstacionDBException("Error al leer la información de la tabla Tipo ID.", ex);                
            }
        }
        
        private void conectar()
        {
            try
            {
                if (con == null)
                {
                    con = ConnectionHelper.createAppConnection();
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
