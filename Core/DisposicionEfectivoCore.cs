using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.DAO;
using EstacionDB.Exceptions;
using EstacionDB.DTO;
using EstacionDB.VO;

namespace EstacionDB.Core
{
    public class DisposicionEfectivoCore
    {
        //Atributos
        private volatile static DisposicionEfectivoCore instance;
        private EgresosDAO egresosDAO;
        private CierreVentasDAO cierresDAO;
        //Constructor
        public DisposicionEfectivoCore()
        {
            egresosDAO = new EgresosDAO();
            cierresDAO = new CierreVentasDAO();
        }
        //Métodos
        /// <summary>
        /// Obtiene la instancia de DisposicionEfectivoCore
        /// </summary>
        /// <returns>Instancia de DisposicionEfectivoCore</returns>
        public static DisposicionEfectivoCore getInstance()
        {
            if (instance == null)
            {
                instance = new DisposicionEfectivoCore();
            }
            return instance;
        }
        /// <summary>
        /// <para>Retorna el total del efectivo a disponer para la fecha indicada.</para>
        /// <para>CierreException.  Si la operación falla.</para>
        /// </summary>
        /// <param name="fecha">DateTime, fecha del cierre.</param>
        /// <returns>El total del efectivo a disponer.</returns>
        public double getTotalEfectivoFecha(DateTime fecha)
        {
            try
            {
                return cierresDAO.consultarTotalMedioPago("Efectivo", fecha, fecha);
            }
            catch (EstacionDBException ex)
            {
                throw new CierreException("No se pudo obtener la información del efectivo a disponer", ex);
            }
        }
        /// <summary>
        /// <para>Retorna el total del efectivo dispuesto para la fecha indicada.</para>
        /// <para>CierreException.  Si la operación falla.</para>
        /// </summary>
        /// <param name="fecha">DateTime,  fecha a la que se aplicaron lo egresos.</param>
        /// <returns>El total de efectivo dispuesto.</returns>
        public double getTotalEfectivoDispuestoFecha(DateTime fecha)
        {
            try
            {
                return egresosDAO.consultarTotalEgresosAplicados(fecha, fecha);
            }
            catch (EstacionDBException ex)
            {
                throw new CierreException("No se pudo obtener información del efectivo dispuesto", ex);
            }
        }
        /// <summary>
        /// <para>Obtien la lista de egresos aplicados en la fecha indicada.</para>
        /// <para>CierreException.  Si la operación falla.</para>
        /// </summary>
        /// <param name="fecha">DateTime,  fecha a la que se aplicaron lo egresos.</param>
        /// <returns>Lista de DTO con la información de los egresos.</returns>
        public IList<EgresoDTO> consultarEgresosAplicados(DateTime fecha)
        {
            try
            {
                IList<EgresoDTO> dtos = new List<EgresoDTO>();
                IList<EgresoVO> vos = egresosDAO.consultarEgresosAplicados(fecha, fecha);
                
                foreach (EgresoVO vo in vos)
                {
                    EgresoDTO dto = new EgresoDTO();
                    dto.IdEgreso = vo.IdEgreso;
                    dto.TipoDocumento = vo.Documento;
                    dto.Numero = vo.Numero;
                    dto.Beneficiario = vo.Beneficiario;
                    dto.Valor = vo.Valor;
                    dto.Fecha = vo.Fecha;
                    dto.FechaAplica = vo.FechaAplica;

                    dtos.Add(dto);
                }

                return dtos;
            }
            catch (EstacionDBException ex)
            {
                throw new CierreException("No se pudo obtener los egresos aplicados a la fecha indicada", ex);
            }
        }
    }
}
