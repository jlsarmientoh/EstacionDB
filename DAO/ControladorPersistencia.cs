using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.Exceptions;
using EstacionDB.VO;
using System.Collections;

namespace EstacionDB.DAO
{
    public class ControladorPersistencia
    {
        #region definición de los DAO's
        private VentasDAO ventasDAO = null;
        private LecturasDAO lecturasDAO = null;
        private ClientesDAO clientesDAO = null;
        private TipoIdDAO tipoDAO = null;
        private BancosDAO bancosDAO = null;
        private IngresosDAO ingresosDAO = null;
        private EgresosDAO egresosDAO = null;
        private CierreVentasDAO cierreVentasDAO = null;
        private EmpleadoDAO empleadosDAO = null;
        #endregion
        #region métodos para obtener instancias de los DAO's
        private VentasDAO getVentasDAO()
        {
            if (ventasDAO == null)
            {
                ventasDAO = new VentasDAO();
            }
            return ventasDAO;
        }

        private LecturasDAO getLecturasDAO()
        {
            if (lecturasDAO == null)
            {
                lecturasDAO =  new LecturasDAO();
            }
            return lecturasDAO;
        }

        private ClientesDAO getClientesDAO()
        {
            if (clientesDAO == null)
            {
                clientesDAO = new ClientesDAO();
            }
            return clientesDAO;
        }

        private TipoIdDAO getTiposDAO()
        {
            if (tipoDAO == null)
            {
                tipoDAO = new TipoIdDAO();
            }
            return tipoDAO;
        }

        private BancosDAO getBancosDAO()
        {
            if (bancosDAO == null)
            {
                bancosDAO = new BancosDAO();
            }
            return bancosDAO;
        }

        private IngresosDAO getIngresosDAO()
        {
            if (ingresosDAO == null)
            {
                ingresosDAO = new IngresosDAO();
            }
            return ingresosDAO;
        }

        private EgresosDAO getEgresosDAO()
        {
            if (egresosDAO == null)
            {
                egresosDAO = new EgresosDAO();
            }
            return egresosDAO;
        }

        private CierreVentasDAO getCierreDAO()
        {
            if (cierreVentasDAO == null)
            {
                cierreVentasDAO = new CierreVentasDAO();
            }
            return cierreVentasDAO;
        }

        private EmpleadoDAO getEmpleadoDAO()
        {
            if (empleadosDAO == null)
            {
                empleadosDAO = new EmpleadoDAO();
            }
            return empleadosDAO;
        }
        #endregion


        public List<VentaVO> consultarVentasFidelizados(long codEmp, DateTime fecha1, DateTime fecha2, long isla, long turno)
        {
            try
            {
                int[] islas = null;
                switch (isla)
                {
                    case 1:
                        {
                            islas = new int[]{1,2};
                            break;
                        }
                    case 2:
                        {
                            islas = new int[] { 3, 4 };
                            break;
                        }
                }
                return getVentasDAO().consultarVentasFidelizados(codEmp, fecha1, fecha2, islas, int.Parse(turno.ToString()));
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error en la consulta ventas en DB estación.",ex);
            }
        }

        public List<VentaVO> consultarVentasNoFidelizados(long codEmp, DateTime fecha1, DateTime fecha2, long isla, long turno)
        {
            try
            {
                int[] islas = null;
                switch (isla)
                {
                    case 1:
                        {
                            islas = new int[] { 1, 2 };
                            break;
                        }
                    case 2:
                        {
                            islas = new int[] { 3, 4 };
                            break;
                        }
                }
                return getVentasDAO().consultarVentasNoFidelizados(codEmp, fecha1, fecha2, islas, int.Parse(turno.ToString()));
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error en la consulta ventas en DB estación.", ex);
            }
        }

        public VentaVO consultarVenta(long nroTiquete)
        {
            try
            {
                return getVentasDAO().consultarVentasByTiquete(nroTiquete);
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error en la consulta ventas en DB estación.", ex);
            }
        }

        public VentaVO consultarVentaTurno(long nroTiquete, DateTime fecha1, DateTime fecha2, int isla, int turno)
        {
            try
            {
                int[] islas = null;
                switch (isla)
                {
                    case 1:
                        {
                            islas = new int[] { 1, 2 };
                            break;
                        }
                    case 2:
                        {
                            islas = new int[] { 3, 4 };
                            break;
                        }
                }
                return getVentasDAO().consultarVentasByTiqueteTurno(nroTiquete,fecha1,fecha2, islas, turno);
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error en la consulta ventas en DB estación.", ex);
            }
        }

        public double consultarTotalVentasFidelizadas(long codEmpleado, long turno, long isla, DateTime fecha1, DateTime fecha2)
        {
            try
            {
                string islas = null;
                switch (isla)
                {
                    case 1:
                        {
                            islas = Utilidades.Utilidades.grupo1;
                            break;
                        }
                    case 2:
                        {
                            islas = Utilidades.Utilidades.grupo2;
                            break;
                        }
                }
                return getVentasDAO().consultarTotalVentasFidelizados(codEmpleado, turno, islas, fecha1, fecha2);
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error en la consulta ventas en DB estación.", ex);
            }
        }

        public List<LecturaVO> consultarLecturas(DateTime fecha1, DateTime fecha2)
        {
            try
            {
                return getLecturasDAO().consultarLecturas(fecha1, fecha2);
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error en la consulta lecturas en DB estación.", ex);
            }
        }

        public List<ClienteVO> consultarClientes()
        {
            try
            {
                return getClientesDAO().consultarClientes();
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error en la consulta clientes en DB app.", ex);
            }
        }

        public ClienteVO consultarCliente(long idCliente)
        {
            try
            {
                return getClientesDAO().consultarClientesById(idCliente);
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error en la consulta clientes en DB app.", ex);
            }
        }

        public List<TipoIdVO> consultarTipos()
        {
            try
            {
                return getTiposDAO().consultarTipos();
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error en la consulta tipos id en DB app.", ex);
            }
        }

        public bool guardarCliente(ClienteVO cliente)
        {
            bool result = false;
            try
            {
                int rows = getClientesDAO().guardarCliente(cliente);
                if (rows > 0)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error en la actualizacion de cliente en DB app.", ex);
            }

        }

        public int guardarVentasDia(List<VentaVO> ventas)
        {
            try
            {
                return getVentasDAO().guardarVentas(ventas);
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error en la actualizacion de las ventas en DB app.", ex);
            }
        }

        public List<BancoVO> consultarBancos()
        {
            try
            {
                return getBancosDAO().consultarBancos();
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error en la consulta bancos en DB app.", ex);
            }
        }

        public int guardarIngresos(List<IngresoVO> ingresos, DateTime fecha)
        {
            try
            {
                List<IngresoVO> tmp = getIngresosDAO().consultarIngresosFecha(fecha, fecha.AddDays(1));
                if (tmp.Count > 0)
                {
                    throw new CierreException("Ya se ha realizado cierre para la fecha seleccionada");
                }
                return getIngresosDAO().guardarIngresos(ingresos, fecha);
            }
            catch (EstacionDBException ex)
            {
                throw new EstacionDBException("No se pudo realizar el cierre de ingresos para la fecha seleccionada", ex);
            }
        }

        public int guardarEgresos(List<EgresoVO> egresos, DateTime fecha, DateTime fechaAplica)
        {
            try
            {   
                return getEgresosDAO().guardarEgresos(egresos, fecha, fechaAplica);
            }
            catch (EstacionDBException ex)
            {
                throw new EstacionDBException("No se pudo realizar el cierre de egresos para la fecha seleccionada", ex);
            }
        }

        public int existeCierre(long codEmpleado, long turno, long isla, DateTime fecha1, DateTime fecha2)
        {
            try
            {
                return getCierreDAO().consultarCuentaCierres(codEmpleado, turno, isla, fecha1, fecha2);
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error en la consulta cierres en DB Expo.", ex);
            }
        }

        public double consultarTotalEfectivoFecha(DateTime fecha1, DateTime fecha2)
        {
            double total = 0;
            IList cierres = null;

            cierres = getCierreDAO().consultarCierres(fecha1, fecha2);


            if (cierres != null)
            {
                foreach (CierreVentasVO cierre in cierres)
                {
                    total += cierre.Efectivo;
                }
            }

            return total;
        }

        public double consultarTotalEgresosAplicadosFecha(DateTime fecha1, DateTime fecha2)
        {
            double total = 0;
            List<EgresoVO> egresos = null;

            egresos = getEgresosDAO().consultarEgresosAplicadosFecha(fecha1, fecha2);


            if (egresos != null)
            {
                foreach (EgresoVO egreso in egresos)
                {
                    total += egreso.Valor;
                }
            }

            return total;
        }

        public int guardarCierre(CierreVentasVO cierre)
        {
            try
            {
                return getCierreDAO().guardarCierre(cierre);
            }
            catch (EstacionDBException ex)
            {
                throw new EstacionDBException("No se pudo realizar el cierre de ventas para la fecha seleccionada", ex);
            }
        }

        public IList getEmpleados()
        {
            try
            {
                return getEmpleadoDAO().consultarEmpleados();
            }
            catch (EstacionDBException ex)
            {
                throw new EstacionDBException("Error en la consulta de empleados", ex);
            }
        }

        public List<VentaTurnoVO> consultarVentasTurno(long codEmp, DateTime fecha1, DateTime fecha2, long isla, long turno)
        {
            try
            {
                int[] islas = null;
                switch (isla)
                {
                    case 1:
                        {
                            islas = new int[] { 1, 2 };
                            break;
                        }
                    case 2:
                        {
                            islas = new int[] { 3, 4 };
                            break;
                        }
                }
                return getVentasDAO().consultarVentasTurno(codEmp, fecha1, fecha2, islas, int.Parse(turno.ToString()));
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error en la consulta ventas en DB estación.", ex);
            }
        }

        public int guardarVentasTurno(List<VentaTurnoVO> ventas)
        {
            try
            {
                return getVentasDAO().guardarVentasTurno(ventas);
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error en la actualizacion de las ventas en DB app.", ex);
            }
        }
    }
}
