using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.DAO;
using EstacionDB.DTO;
using EstacionDB.Exceptions;
using EstacionDB.VO;

namespace EstacionDB.Core
{
    public class CierreEfectivoCore
    {
        //Atributos
        private volatile static CierreEfectivoCore instance;
        private CierreEfectivoDAO cierreEfectivoDAO;
        //Constructor
        private CierreEfectivoCore()
        {
            cierreEfectivoDAO = new CierreEfectivoDAO();
        }
        //Métodos
        /// <summary>
        /// Obtiene la instancia de CierreEfectivoCore
        /// </summary>
        /// <returns>Instancia de CierreEfectivoCore</returns>
        public static CierreEfectivoCore getInstance()
        {
            if (instance == null)
            {
                instance = new CierreEfectivoCore();
            }
            return instance;
        }

        /// <summary>
        /// Guarda o actualiza un cierre de efectivo.
        /// </summary>
        /// <param name="dto">Objeto DTO con la información del cierre de venta.</param>
        /// <returns></returns>
        public int guardarCierre(CierreEfectivoDTO dto)
        {
            try
            {
                CierreEfectivoVO vo = new CierreEfectivoVO();
                vo.Efectivo = dto.Efectivo;
                vo.Egresos = dto.Egresos;
                vo.Estado = dto.Estado;
                vo.Fecha = dto.Fecha;
                vo.IdCierre = 0;

                return cierreEfectivoDAO.guardarCierre(vo);
            }
            catch (EstacionDBException ex)
            {
                throw new CierreException("No se pudo obtener la información del cierre de efectivo", ex);
            }
        }

        /// <summary>
        /// <para>Comprueba si existen cierres de efectivo para el rango de fechas recibido como parámetro.</para>
        /// </summary>
        /// <param name="fecha1">Fecha de inicio.</param>
        /// <param name="fecha2">Fecha final.</param>
        /// <returns>True Si existen cierres para el rango de fecha recibido como parámetro, de los contrario retorna False</returns>
        public bool existeCierre(DateTime fecha1, DateTime fecha2)
        {
            return cierreEfectivoDAO.contarCierres(fecha1, fecha2) > 0;
        }
    }
}
