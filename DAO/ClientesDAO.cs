using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.VO;
using System.Data.SqlClient;
using EstacionDB.Exceptions;
using EstacionDB.Helper;

namespace EstacionDB.DAO
{
    public class ClientesDAO
    {
        private SqlConnection con;

        public List<ClienteVO> consultarClientes()
        {
            List<ClienteVO> clientes = new List<ClienteVO>();
            try
            {
                #region  se abre la conexión con la BD
                conectar(Utilidades.Utilidades.appCadenaConexion);
                #endregion

                #region se preparan los objetos para hacer la consulta y leerla
                SqlDataReader reader = null;
                SqlCommand query = new SqlCommand("SELECT [ID_CLIENTE],[TIPO_ID],[IDENTIFICACION],[NOMBRE],[DIRECCION],[TELEFONO],[CONTACTO],[EMAIL] FROM [CLIENTES] ORDER BY NOMBRE", con);
                #endregion

                #region se ejecuta el query, se lee el resultado y se procesa en el VO;
                reader = query.ExecuteReader();
                if (reader != null)
                {
                    // Si tiene reaultados los recorre fila por fila
                    while (reader.Read())
                    {
                        ClienteVO tmpCliente = new ClienteVO();
                        if (reader["ID_CLIENTE"] != null) tmpCliente.IdCliente = long.Parse(reader["ID_CLIENTE"].ToString());
                        if (reader["TIPO_ID"] != null) tmpCliente.TipoId = long.Parse(reader["TIPO_ID"].ToString());
                        if (reader["IDENTIFICACION"] != null) tmpCliente.Identificacion = reader["IDENTIFICACION"].ToString();
                        if (reader["NOMBRE"] != null) tmpCliente.Nombre = reader["NOMBRE"].ToString();
                        if (reader["DIRECCION"] != null) tmpCliente.Direccion = reader["DIRECCION"].ToString();
                        if (reader["TELEFONO"] != null) tmpCliente.Telefono = reader["TELEFONO"].ToString();
                        if (reader["CONTACTO"] != null) tmpCliente.Contacto = reader["CONTACTO"].ToString();
                        if (reader["EMAIL"] != null) tmpCliente.Email = reader["EMAIL"].ToString();

                        clientes.Add(tmpCliente);
                    }
                }
                #endregion

                desconectar();
                return clientes;
            
            }
            catch (System.Exception ex)
            {
                desconectar();
                throw new EstacionDBException("Error al leer la información de la tabla clientes.",ex);                
            }
        }

        public ClienteVO consultarClientesById(long idCliente)
        {
            ClienteVO tmpCliente = null;
            try
            {
                #region  se abre la conexión con la BD
                conectar(Utilidades.Utilidades.appCadenaConexion);
                #endregion

                #region se preparan los objetos para hacer la consulta y leerla
                SqlDataReader reader = null;
                SqlCommand query = new SqlCommand("SELECT [ID_CLIENTE],[TIPO_ID],[IDENTIFICACION],[NOMBRE],[DIRECCION],[TELEFONO],[CONTACTO],[EMAIL] FROM [CLIENTES] WHERE [ID_CLIENTE] = " + idCliente, con);
                #endregion

                #region se ejecuta el query, se lee el resultado y se procesa en el VO;
                reader = query.ExecuteReader();
                if (reader != null)
                {
                    // Si tiene reaultados los recorre fila por fila
                    while (reader.Read())
                    {
                        tmpCliente = new ClienteVO();
                        if (reader["ID_CLIENTE"] != null) tmpCliente.IdCliente = long.Parse(reader["ID_CLIENTE"].ToString());
                        if (reader["TIPO_ID"] != null) tmpCliente.TipoId = long.Parse(reader["TIPO_ID"].ToString());
                        if (reader["IDENTIFICACION"] != null) tmpCliente.Identificacion = reader["IDENTIFICACION"].ToString();
                        if (reader["NOMBRE"] != null) tmpCliente.Nombre = reader["NOMBRE"].ToString();
                        if (reader["DIRECCION"] != null) tmpCliente.Direccion = reader["DIRECCION"].ToString();
                        if (reader["TELEFONO"] != null) tmpCliente.Telefono = reader["TELEFONO"].ToString();
                        if (reader["CONTACTO"] != null) tmpCliente.Contacto = reader["CONTACTO"].ToString();
                        if (reader["EMAIL"] != null) tmpCliente.Email = reader["EMAIL"].ToString();

                    }
                }
                #endregion

                desconectar();
                return tmpCliente;

            }
            catch (System.Exception ex)
            {
                desconectar();
                throw new EstacionDBException("Error al leer la información de la vista Ventas.", ex);                
            }
        }

        public int guardarCliente(ClienteVO cliente)
        {
            try
            {
                #region  se abre la conexión con la BD
                conectar(Utilidades.Utilidades.appCadenaConexion);
                #endregion

                #region se preparan los objetos para hacer el inserto o el update dependiendo del atibuto idCliente, si es = 0 insert; si es != 0 update                
                SqlCommand query = null;
                if (cliente.IdCliente == 0)
                {
                    query = new SqlCommand("INSERT INTO [CLIENTES] " +
                    "([TIPO_ID]" +
                    ",[IDENTIFICACION]" +
                    ",[NOMBRE]" +
                    ",[DIRECCION]" +
                    ",[TELEFONO]" +
                    ",[CONTACTO]" +
                    ",[EMAIL]) " +
                    "VALUES(" + cliente.TipoId +
                    ",'" + cliente.Identificacion +
                    "','" + cliente.Nombre +
                    "','" + cliente.Direccion +
                    "','" + cliente.Telefono +
                    "','" + cliente.Contacto +
                    "','" + cliente.Email + "')", con);
                }
                else
                {
                    query = new SqlCommand("UPDATE [CLIENTES] SET " +
                     "[TIPO_ID] = " + cliente.TipoId +
                     ",[IDENTIFICACION] = '" + cliente.Identificacion +
                     "',[NOMBRE] = '" + cliente.Nombre +
                     "',[DIRECCION] = '" + cliente.Direccion +
                     "',[TELEFONO] = '" + cliente.Telefono +
                     "',[CONTACTO] = '" + cliente.Contacto +
                     "',[EMAIL] = '" + cliente.Email + "'" +
                     " WHERE [ID_CLIENTE] = " + cliente.IdCliente , con);
                }
                
                #endregion

                #region se ejecuta el query, se lee el resultado
                int rows = query.ExecuteNonQuery();                
                #endregion

                desconectar();
                return rows;

            }
            catch (System.Exception ex)
            {
                desconectar();
                throw new EstacionDBException("Error al leer la información de la tabla clientes.", ex);
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
