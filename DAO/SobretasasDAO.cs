using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using EstacionDB.VO;
using EstacionDB.Helper;
using NHibernate.Criterion;
using System.Collections;
using EstacionDB.Exceptions;

namespace EstacionDB.DAO
{
    public class SobretasasDAO
    {
        
        public int guardarCierre(SobretasaVO sobretasa)
        {
            int rows = 0;
            ITransaction tx = null;

            try
            {
                ISession session = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo);
                tx = session.BeginTransaction();

                session.Save(sobretasa);

                tx.Commit();
                rows++;

                ConnectionHelper.CloseSession();

                return rows;

            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la tabla sobretasas.", ex);
            }
        }

        public List<SobretasaVO> consultarSobretasas(int mes, int anio, int dia)
        {
            List<SobretasaVO> sobretasas = new List<SobretasaVO>();
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(SobretasaVO))
                    .Add(Expression.Eq("Mes", mes))
                    .Add(Expression.Eq("Anio", anio))
                    .Add(Expression.Le("DiaInicioVigencia", dia))
                    .Add(Expression.Ge("DiaFinVigenica", dia));

                IList tmp = criteria.List();

                foreach (SobretasaVO sobretasa in tmp)
                {
                    sobretasas.Add(sobretasa);
                }

                ConnectionHelper.CloseSession();

                return sobretasas;
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la vista sobretasas.", ex);
            }
        }

        public SobretasaVO consultarSobretasaProducto(int mes, int anio, int idProducto, int dia)
        {
            try
            {
                ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(SobretasaVO))
                    .Add(Expression.Eq("Mes", mes))
                    .Add(Expression.Eq("Anio", anio))
                    .Add(Expression.Eq("IdProducto", anio))
                    .Add(Expression.Le("DiaInicioVigencia", dia))
                    .Add(Expression.Ge("DiaFinVigenica", dia));

                SobretasaVO tmp = criteria.UniqueResult<SobretasaVO>();
                ConnectionHelper.CloseSession();

                return tmp;
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();
                throw new EstacionDBException("Error al leer la información de la vista sobretasas.", ex);
            }
        }
    }
}
