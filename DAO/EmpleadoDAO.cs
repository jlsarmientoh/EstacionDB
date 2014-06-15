using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using EstacionDB.VO;
using EstacionDB.Exceptions;
using EstacionDB.Helper;
using System.Collections;
using NHibernate.Criterion;

namespace EstacionDB.DAO
{
    public class EmpleadoDAO
    {
        public IList consultarEmpleados()
        {
            IList emps = null;
            try
            {
                emps = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configServ).CreateCriteria(typeof(VO.EmpleadoVO)).AddOrder(Order.Asc("Nombre")).List();
                ConnectionHelper.CloseSession();
                return emps;
            }
            catch (System.Exception ex)
            {
                ConnectionHelper.CloseSession();                
                throw new EstacionDBException("Error al leer la información de la tabla Empleado.", ex);                
            }
        }
    }
}
