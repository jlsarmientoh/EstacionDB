using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.VO;
using System.Data.SqlClient;
using EstacionDB.Exceptions;
using EstacionDB.Helper;
using NHibernate;
using NHibernate.Criterion;
using System.Collections;

namespace EstacionDB.DAO
{
    public class ClientesDAO
    {
        public List<ClienteVO> consultarClientes()
        {
            List<ClienteVO> clientes = new List<ClienteVO>();
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(ClienteVO))
                    .AddOrder(Order.Asc("Nombre"));

                IList tmp = criteria.List();
                foreach (ClienteVO cliente in tmp)
                {
                    clientes.Add(cliente);
                }
                ConnectionHelper.CloseSession();
                
                return clientes;
            
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la tabla clientes.",ex);                
            }
        }

        public List<ClienteVO> consultarClientesServP()
        {
            List<ClienteVO> clientes = new List<ClienteVO>();
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configServ).CreateCriteria(typeof(ClienteVO))
                    .AddOrder(Order.Asc("Nombre"));

                IList tmp = criteria.List();
                foreach (ClienteVO cliente in tmp)
                {
                    clientes.Add(cliente);
                }
                ConnectionHelper.CloseSession();

                return clientes;

            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la tabla clientes.", ex);
            }
        }


        public ClienteVO consultarClientesById(long idCliente)
        {
            ClienteVO tmpCliente = null;
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(ClienteVO))
                    .Add(Expression.Eq("Id", idCliente));

                tmpCliente = criteria.UniqueResult<ClienteVO>();

                ConnectionHelper.CloseSession();
                
                return tmpCliente;

            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la vista Ventas.", ex);                
            }
        }

        public ClienteVO consultarClienteByCodigo(string codigo)
        {
            ClienteVO tmpCliente = null;
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(ClienteVO))
                    .Add(Expression.Eq("Codigo", codigo));

                tmpCliente = criteria.UniqueResult<ClienteVO>();

                ConnectionHelper.CloseSession();

                return tmpCliente;

            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la vista Ventas.", ex);
            }
        }

        public int guardarCliente(ClienteVO cliente)
        {
            int rows = 0;
            ITransaction tx = null;
            try
            {
                ISession session = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo);
                tx = session.BeginTransaction();
                if (cliente.IdCliente == 0)
                {
                    session.Save(cliente);
                }
                else
                {
                    session.Update(cliente);
                }
                
                rows++;
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
                throw new EstacionDBException("Error al leer la información de la tabla clientes.", ex);
            }
        }
    }
}
