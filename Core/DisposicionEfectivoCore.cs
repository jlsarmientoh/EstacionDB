using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.DAO;
using EstacionDB.Exceptions;
using EstacionDB.DTO;
using EstacionDB.VO;
using System.Collections;

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
                IList vos = egresosDAO.consultarEgresosAplicados(fecha, fecha);
                
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

        public int eliminarEgreso(EgresoDTO egreso)
        {
            try
            {
                EgresoVO e = new EgresoVO();
                e.Beneficiario = egreso.Beneficiario;
                e.Documento = egreso.TipoDocumento;
                e.Fecha = egreso.Fecha;
                e.FechaAplica = egreso.FechaAplica;
                e.IdEgreso = egreso.IdEgreso;
                e.Isla = 0;
                e.Numero = egreso.Numero;
                e.Valor = egreso.Valor;

                return egresosDAO.eliminarEgreso(e);
            }
            catch (EstacionDBException ex)
            {
                throw new CierreException("No se pudo eliminar el egreso", ex);
            }
        }

        public int guardarEgresos(IList egresos)
        {
            try
            {
                IList<EgresoVO> vos = new List<EgresoVO>();
                foreach (EgresoDTO dto in egresos)
                {
                    EgresoVO e = new EgresoVO();
                    e.Beneficiario = dto.Beneficiario;
                    e.Documento = dto.TipoDocumento;
                    e.Fecha = dto.Fecha;
                    e.FechaAplica = dto.FechaAplica;
                    e.IdEgreso = dto.IdEgreso;
                    e.Isla = 0;
                    e.Numero = dto.Numero;
                    e.Valor = dto.Valor;

                    vos.Add(e);
                }

                return egresosDAO.guardarEgresos(vos);
            }
            catch (EstacionDBException ex)
            {
                throw new CierreException("No se pudo acutalizar los egresos", ex);
            }
        }
    }
}
