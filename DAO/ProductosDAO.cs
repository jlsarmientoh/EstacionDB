using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using EstacionDB.Helper;
using System.Collections;
using EstacionDB.VO;
using EstacionDB.Exceptions;

namespace EstacionDB.DAO
{
     public class ProductosDAO
    {
         public List<ProductoVO> consultarProductos()
         {
             List<ProductoVO> productos = new List<ProductoVO>();
             try
             {
                 ICriteria criteria = ConnectionHelper.getCurrentSession(Utilidades.Utilidades.configExpo).CreateCriteria(typeof(ProductoVO));
                 IList tmp = criteria.List();

                 foreach (ProductoVO p in tmp)
                 {
                     productos.Add(p);
                 }
                 ConnectionHelper.CloseSession();

                 return productos;
             }
             catch (System.Exception ex)
             {
                 ConnectionHelper.CloseSession();
                 throw new EstacionDBException("Error al leer la información de la tabla productos.", ex);
             }

         }
    }
}
