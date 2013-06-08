using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.VO;
using NHibernate;
using EstacionDB.Helper;
using EstacionDB.Exceptions;
using NHibernate.Criterion;
using System.Collections;

namespace EstacionDB.DAO
{
    public class CompraCombustibleDAO
    {
        public int guardar(CompraCombustibleVO c) 
        {
            int rows = 0;
            ITransaction tx = null;

            try
            {
                ISession session = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo);
                tx = session.BeginTransaction();

                if (c.IdCompra == 0)
                {
                    session.Save(c);
                }
                else
                {
                    session.Update(c);
                }

                tx.Commit();
                rows++;

                ConnectionHelper.CloseSession();

                return rows;
            }
            catch (Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al guardar la información en la tabla Compras combustible", ex);
            }
        }

        public List<CompraCombustibleVO> consultarComprasFecha(DateTime fecha1, DateTime fecha2)
        {
            List<CompraCombustibleVO> compras = new List<CompraCombustibleVO>();
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(CompraCombustibleVO))
                    .Add(Expression.Between("Fecha", fecha1, fecha2));

                IList tmp = criteria.List();

                foreach (CompraCombustibleVO c in tmp)
                {
                    compras.Add(c);
                }

                ConnectionHelper.CloseSession();

                return compras;
            }
            catch (Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la tabla compas combustible", ex);
            }
        }

        public double consultarComprasGalonesFechaProducto(long idProducto, DateTime fecha1, DateTime fecha2)
        {
            double galones = 0;
            try
            {
                string sqlQuery = "Select sum(c.Galones) From EstacionDB.VO.CompraCombustibleVO c Where Fecha Between :Fecha1 And :Fecha2 And IdProducto = :idProducto";
                IQuery query = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateQuery(sqlQuery);
                query.SetParameter("Fecha1", fecha1);
                query.SetParameter("Fecha2", fecha2);
                query.SetParameter("idProducto", idProducto);
                object tmp = query.UniqueResult();

                if (tmp != null)
                {
                    galones = double.Parse(tmp.ToString());
                }

                ConnectionHelper.CloseSession();

                return galones;
            }
            catch (Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la tabla compras combustible", ex);
            }
        }
    }
}
