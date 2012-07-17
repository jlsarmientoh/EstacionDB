using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using NHibernate.Cfg;
using NHibernate;


namespace EstacionDB.Helper
{
    public sealed class ConnectionHelper
    {

        private static ISessionFactory sessionFactory;
        private static ISession currentSession;

        static ConnectionHelper()
        {
            
        }

        public static SqlConnection createConecction(string cadenaConexion)
        {
            return new SqlConnection(cadenaConexion);
        }

        public static SqlDataAdapter createDataAdapter()
        {
            return new SqlDataAdapter();
        }

        public static SqlDataAdapter createDataAdapter(string sql, SqlConnection con)
        {
            return new SqlDataAdapter(sql, con);
        }
        
        public static SqlConnection createDafaultConnection()
        {
            return createConecction(Utilidades.Utilidades.cadenaConexion);
        }

        public static SqlConnection createAppConnection()
        {
            return createConecction(Utilidades.Utilidades.appCadenaConexion);
        }

        public static ISession getCurrentSession(string configFile)
        {   
            currentSession = null;
            try
            {
                sessionFactory = new Configuration().Configure(configFile).BuildSessionFactory();
                currentSession = sessionFactory.OpenSession();                
                return currentSession;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                currentSession.Close();
                return null;
            }
        }

        public static void CloseSession()
        {
            if (currentSession == null)
            {
                // No current session
                return;
            }

            currentSession.Close();
        }

        public static void CloseSessionFactory()
        {
            if (sessionFactory != null)
            {
                sessionFactory.Close();
            }
        }
    }
}
