using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.VO;
using System.Data.SqlClient;
using EstacionDB.Exceptions;
using EstacionDB.Helper;

namespace EstacionDB.DAO
{
    public class BancosDAO
    {
        private SqlConnection con;

        public List<BancoVO> consultarBancos()
        {
            List<BancoVO> tipos = new List<BancoVO>();
            try
            {
                #region  se abre la conexión con la BD
                conectar();
                #endregion

                #region se preparan los objetos para hacer la consulta y leerla
                SqlDataReader reader = null;
                SqlCommand query = new SqlCommand("SELECT [ID_BANCO],[NOMBRE] FROM [BANCOS]", con);
                #endregion

                #region se ejecuta el query, se lee el resultado y se procesa en el VO;
                reader = query.ExecuteReader();
                if (reader != null)
                {
                    // Si tiene reaultados los recorre fila por fila
                    while (reader.Read())
                    {
                        BancoVO tmpTipo = new BancoVO();
                        if (reader["ID_BANCO"] != null) tmpTipo.IdBanco = long.Parse(reader["ID_BANCO"].ToString());
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
                throw new EstacionDBException("Error al leer la información de la tabla Bancos.", ex);                
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
