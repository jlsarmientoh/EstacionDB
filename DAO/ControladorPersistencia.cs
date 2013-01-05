using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.Exceptions;
using EstacionDB.VO;
using System.Collections;
using EstacionDB.DTO;
using System.Globalization;

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
        private ProductosTurnoDAO productosTurnoDAO = null;
        private SobretasasDAO sobretasasDAO = null;
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

        private ProductosTurnoDAO getProductosTurnoDAO()
        {
            if (productosTurnoDAO == null)
            {
                productosTurnoDAO = new ProductosTurnoDAO();
            }
            return productosTurnoDAO;
        }

        private SobretasasDAO getSobreTasasDAO()
        {
            if (sobretasasDAO == null)
            {
                sobretasasDAO = new SobretasasDAO();
            }
            return sobretasasDAO;
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

        public List<VentaVO> consultarVentasAgrupadas(DateTime fecha1, DateTime fecha2)
        {
            try
            {   
                return getVentasDAO().consultarVentasAgrupadas(fecha1, fecha2);
            }
            catch (EstacionDBException ex)
            {
                throw new PersistenciaException("Error en la consulta ventas en DB estación.", ex);
            }
        }

        public List<MovimientoContableDTO> getMovimientosContables(DateTime fecha1, DateTime fecha2, string doc)
        {
            List<MovimientoContableDTO> movimientos = new List<MovimientoContableDTO>();

            try
            {
                double sobretasaCorriente = 0;
                double sobretasaSuper = 0;
                double sobretasaDiesel = 0;
                double subtotalSobretasaCorriente = 0;
                double subtotalSobretasaSuper = 0;
                double subtotalSobretasaDiesel = 0;
                double totalCreditos = 0;
                double totalDebitos = 0;
                List<VentaVO> ventasCredito = getVentasDAO().consultarVentasAgrupadas(fecha1, fecha2);
                List<CierreVentasVO> cierresVenta = getCierreDAO().consultarCierresAgrupados(fecha1, fecha2);
                List<SobretasaVO> sobretasas = getSobreTasasDAO().consultarSobretasas(fecha1.Month, fecha1.Year, fecha1.Day);
                List<ProductoTurnoVO> productos = getProductosTurnoDAO().consultarProductosAgrupados(fecha1, fecha2);
                CierreVentasVO cv = cierresVenta[0];

                #region Movimientos Debito
                foreach (VentaVO vc in ventasCredito)
                {
                    MovimientoContableDTO mc = new MovimientoContableDTO();
                    
                    mc.Fecha = vc.Fecha.ToString("dd/MM/yyyy");
                    mc.TipoDoc = Utilidades.Utilidades.TipoMovimiento;
                    mc.Doc = doc;
                    if (Utilidades.Utilidades.HomologarNits) // Si se trata de una venta fidelizada
                    {
                        try
                        {
                            ClienteVO tmpCliente = getClientesDAO().consultarClienteByCodigo(vc.Nit);
                            if (tmpCliente != null)
                            {
                                mc.Nit = tmpCliente.Identificacion.Trim();
                            }
                            else
                            {
                                NitDTO nitDto = Utilidades.Utilidades.formatearNit(vc.Nit);
                                mc.Nit = nitDto.Nit;
                            }
                        }
                        catch (EstacionDBException ex)
                        {
                            NitDTO nitDto = Utilidades.Utilidades.formatearNit(vc.Nit);
                            mc.Nit = nitDto.Nit;
                        }
                    }
                    else
                    {
                        NitDTO nitDto = Utilidades.Utilidades.formatearNit(vc.Nit);
                        mc.Nit = nitDto.Nit;
                    }
                    
                    mc.Cuenta = Utilidades.Utilidades.CuentaCredito;
                    mc.Naturaleza = Utilidades.Utilidades.NatutalezaDebito;                    
                    mc.Valor = vc.Total.ToString("0.00", CultureInfo.InvariantCulture);
                    mc.CentroCosto = "";
                    mc.CuentaBancaria = "";
                    movimientos.Add(mc);
                }
                                
                // Movimiento contable para Efectivo
                MovimientoContableDTO mc1 = new MovimientoContableDTO();                
                mc1.Fecha = cv.Fecha.ToString("dd/MM/yyyy");
                mc1.TipoDoc = Utilidades.Utilidades.TipoMovimiento;
                mc1.Doc = doc;
                mc1.Nit = "";
                mc1.Nit = Utilidades.Utilidades.NitEDS;
                mc1.Cuenta = Utilidades.Utilidades.CuentaEfectivo;
                mc1.Naturaleza = Utilidades.Utilidades.NatutalezaDebito;
                mc1.Valor = cv.Efectivo.ToString("0.00", CultureInfo.InvariantCulture);
                mc1.CentroCosto = "";
                mc1.CuentaBancaria = "";
                movimientos.Add(mc1);

                // Movimiento contable para Sodexo
                MovimientoContableDTO mc2 = new MovimientoContableDTO();
                mc2.Fecha = cv.Fecha.ToString("dd/MM/yyyy");
                mc2.TipoDoc = Utilidades.Utilidades.TipoMovimiento;
                mc2.Doc = doc;
                mc2.Nit = Utilidades.Utilidades.NitSodexo;
                mc2.Cuenta = Utilidades.Utilidades.CuentaSodexo;
                mc2.Naturaleza = Utilidades.Utilidades.NatutalezaDebito;
                mc2.Valor = cv.Sodexo.ToString("0.00", CultureInfo.InvariantCulture);
                mc2.CentroCosto = "";
                mc2.CuentaBancaria = "";
                movimientos.Add(mc2);

                // Movimiento contable para Big Pass
                MovimientoContableDTO mc3 = new MovimientoContableDTO();
                mc3.Fecha = cv.Fecha.ToString("dd/MM/yyyy");
                mc3.TipoDoc = Utilidades.Utilidades.TipoMovimiento;
                mc3.Doc = doc;
                mc3.Nit = Utilidades.Utilidades.NitBigPass;
                mc3.Cuenta = Utilidades.Utilidades.CuentaBigPass;
                mc3.Naturaleza = Utilidades.Utilidades.NatutalezaDebito;
                mc3.Valor = cv.BigPass.ToString("0.00", CultureInfo.InvariantCulture);
                mc3.CentroCosto = "";
                mc3.CuentaBancaria = "";
                movimientos.Add(mc3);

                // Movimiento contable para Tarjetas de credito
                MovimientoContableDTO mc4 = new MovimientoContableDTO();
                mc4.Fecha = cv.Fecha.ToString("dd/MM/yyyy");
                mc4.TipoDoc = Utilidades.Utilidades.TipoMovimiento;
                mc4.Doc = doc;
                mc4.Nit = "";
                mc4.Nit = Utilidades.Utilidades.NitBanco;
                mc4.Cuenta = Utilidades.Utilidades.CuentaTarjetas;
                mc4.Naturaleza = Utilidades.Utilidades.NatutalezaDebito;
                mc4.Valor = cv.Tarjetas.ToString("0.00", CultureInfo.InvariantCulture);
                mc4.CentroCosto = "";
                mc4.CuentaBancaria = Utilidades.Utilidades.CuentaBancaria;
                movimientos.Add(mc4);

                // Movimiento contable para Tarjeta Plus
                MovimientoContableDTO mc5 = new MovimientoContableDTO();
                mc5.Fecha = cv.Fecha.ToString("dd/MM/yyyy");
                mc5.TipoDoc = Utilidades.Utilidades.TipoMovimiento;
                mc5.Doc = doc;
                mc5.Nit = Utilidades.Utilidades.NitEDS;
                mc5.Cuenta = Utilidades.Utilidades.CuentaTarjetaPlus;
                mc5.Naturaleza = Utilidades.Utilidades.NatutalezaDebito;
                mc5.Valor = cv.TarjetaPlus.ToString("0.00", CultureInfo.InvariantCulture);
                mc5.CentroCosto = "";
                mc5.CuentaBancaria = "";
                movimientos.Add(mc5);

                // Movimiento contable para Ticket Tronik
                MovimientoContableDTO mc6 = new MovimientoContableDTO();
                mc6.Fecha = cv.Fecha.ToString("dd/MM/yyyy");
                mc6.TipoDoc = Utilidades.Utilidades.TipoMovimiento;
                mc6.Doc = doc;
                mc6.Nit = Utilidades.Utilidades.NitTicketTronik;
                mc6.Cuenta = Utilidades.Utilidades.CuentaTicketTronik;
                mc6.Naturaleza = Utilidades.Utilidades.NatutalezaDebito;
                mc6.Valor = cv.TicketTronik.ToString("0.00", CultureInfo.InvariantCulture);
                mc6.CentroCosto = "";
                mc6.CuentaBancaria = "";
                movimientos.Add(mc6);

                // Movimiento contable para Otros
                MovimientoContableDTO mc7 = new MovimientoContableDTO();
                mc7.Fecha = cv.Fecha.ToString("dd/MM/yyyy");
                mc7.TipoDoc = Utilidades.Utilidades.TipoMovimiento;
                mc7.Doc = doc;
                mc7.Nit = Utilidades.Utilidades.NitEDS;
                mc7.Cuenta = Utilidades.Utilidades.CuentaOtros;
                mc7.Naturaleza = Utilidades.Utilidades.NatutalezaDebito;
                mc7.Valor = cv.Otros.ToString("0.00", CultureInfo.InvariantCulture);
                mc7.CentroCosto = "";
                mc7.CuentaBancaria = "";
                movimientos.Add(mc7);
                #endregion

                #region Movimientos credito

                // Sobretasas
                foreach(SobretasaVO st in sobretasas){
                    switch (st.IdProducto)
                    {
                        case 1: {
                            sobretasaCorriente = st.Sobretasa;
                            break; 
                        }
                        case 2: {
                            sobretasaSuper = st.Sobretasa;
                            break; 
                        }
                        case 3: {
                            sobretasaDiesel = st.Sobretasa;
                            break; 
                        }
                    }
                }

                // Movimientos deSobretasas
                foreach (ProductoTurnoVO pt in productos)
                {
                    
                    if (pt.Producto.Trim().Equals("CORRIENTE"))
                    {
                        MovimientoContableDTO mcs = new MovimientoContableDTO();
                        mcs.Fecha = pt.Fecha.ToString("dd/MM/yyyy");
                        mcs.TipoDoc = Utilidades.Utilidades.TipoMovimiento;
                        mcs.Doc = doc;
                        mcs.Nit = Utilidades.Utilidades.NitEDS;
                        mcs.Naturaleza = Utilidades.Utilidades.NatutalezaCredito;
                        mcs.Cuenta = Utilidades.Utilidades.CuentaSobretasaCorriente;
                        subtotalSobretasaCorriente = pt.Galones * sobretasaCorriente;
                        mcs.Valor = subtotalSobretasaCorriente.ToString("0.00", CultureInfo.InvariantCulture);
                        mcs.CentroCosto = "";
                        mcs.CuentaBancaria = "";
                        movimientos.Add(mcs);

                        MovimientoContableDTO mcn = new MovimientoContableDTO();
                        mcn.Fecha = pt.Fecha.ToString("dd/MM/yyyy");
                        mcn.TipoDoc = Utilidades.Utilidades.TipoMovimiento;
                        mcn.Doc = doc;
                        mcn.Nit = Utilidades.Utilidades.NitEDS;
                        mcn.Naturaleza = Utilidades.Utilidades.NatutalezaCredito;
                        mcn.Cuenta = Utilidades.Utilidades.CuentaVentaCorriente;
                        double diffsub = pt.Valor - subtotalSobretasaCorriente;
                        mcn.Valor = diffsub.ToString("0.00", CultureInfo.InvariantCulture);
                        mcn.CentroCosto = "0901";
                        mcn.CuentaBancaria = "";
                        movimientos.Add(mcn);
                    }
                    else if (pt.Producto.Trim().Equals("SUPER"))
                    {
                        MovimientoContableDTO mcs = new MovimientoContableDTO();
                        mcs.Fecha = pt.Fecha.ToString("dd/MM/yyyy");
                        mcs.TipoDoc = Utilidades.Utilidades.TipoMovimiento;
                        mcs.Doc = doc;
                        mcs.Nit = Utilidades.Utilidades.NitEDS;
                        mcs.Naturaleza = Utilidades.Utilidades.NatutalezaCredito;
                        mcs.Cuenta = Utilidades.Utilidades.CuentaSobretasaSuper;
                        subtotalSobretasaSuper = pt.Galones * sobretasaSuper;
                        mcs.Valor = subtotalSobretasaSuper.ToString("0.00", CultureInfo.InvariantCulture);
                        mcs.CentroCosto = "";
                        mcs.CuentaBancaria = "";
                        movimientos.Add(mcs);

                        MovimientoContableDTO mcn = new MovimientoContableDTO();
                        mcn.Fecha = pt.Fecha.ToString("dd/MM/yyyy");
                        mcn.TipoDoc = Utilidades.Utilidades.TipoMovimiento;
                        mcn.Doc = doc;
                        mcn.Nit = Utilidades.Utilidades.NitEDS;
                        mcn.Naturaleza = Utilidades.Utilidades.NatutalezaCredito;
                        mcn.Cuenta = Utilidades.Utilidades.CuentaVentaSuper;
                        double diffsub1 = pt.Valor - subtotalSobretasaSuper;
                        mcn.Valor = diffsub1.ToString("0.00", CultureInfo.InvariantCulture);
                        mcn.CentroCosto = "0901";
                        mcn.CuentaBancaria = "";
                        movimientos.Add(mcn);
                    }
                    else if (pt.Producto.Trim().Equals("DIESEL"))
                    {
                        MovimientoContableDTO mcs = new MovimientoContableDTO();
                        mcs.Fecha = pt.Fecha.ToString("dd/MM/yyyy");
                        mcs.TipoDoc = Utilidades.Utilidades.TipoMovimiento;
                        mcs.Doc = doc;
                        mcs.Nit = Utilidades.Utilidades.NitEDS;
                        mcs.Naturaleza = Utilidades.Utilidades.NatutalezaCredito;
                        mcs.Cuenta = Utilidades.Utilidades.CuentaSobretasaDiesel;
                        subtotalSobretasaDiesel = pt.Galones * sobretasaDiesel;
                        mcs.Valor = subtotalSobretasaDiesel.ToString("0.00", CultureInfo.InvariantCulture);
                        mcs.CentroCosto = "";
                        mcs.CuentaBancaria = "";
                        movimientos.Add(mcs);

                        MovimientoContableDTO mcn = new MovimientoContableDTO();
                        mcn.Fecha = pt.Fecha.ToString("dd/MM/yyyy");
                        mcn.TipoDoc = Utilidades.Utilidades.TipoMovimiento;
                        mcn.Doc = doc;
                        mcn.Nit = Utilidades.Utilidades.NitEDS;
                        mcn.Naturaleza = Utilidades.Utilidades.NatutalezaCredito;
                        mcn.Cuenta = Utilidades.Utilidades.CuentaVentaDiesel;
                        double diffsub2 = pt.Valor - subtotalSobretasaDiesel;
                        mcn.Valor = diffsub2.ToString("0.00", CultureInfo.InvariantCulture);
                        mcn.CentroCosto = "0901";
                        mcn.CuentaBancaria = "";
                        movimientos.Add(mcn);
                    }
                }
                #endregion

                #region totales debito/creditos y validacion de ecuacion contable

                foreach (MovimientoContableDTO mcDto in movimientos)
                {
                    if (mcDto.Naturaleza.Equals(Utilidades.Utilidades.NatutalezaCredito))
                    {
                        totalCreditos += double.Parse(mcDto.Valor, CultureInfo.InvariantCulture);
                    }
                    else if (mcDto.Naturaleza.Equals(Utilidades.Utilidades.NatutalezaDebito))
                    {
                        totalDebitos += double.Parse(mcDto.Valor, CultureInfo.InvariantCulture);
                    }
                }

                if (totalCreditos > totalDebitos)
                {
                    double diff1 = totalCreditos - totalDebitos;
                    MovimientoContableDTO mca = new MovimientoContableDTO();
                    mca.Fecha = fecha1.ToString("dd/MM/yyyy");
                    mca.TipoDoc = Utilidades.Utilidades.TipoMovimiento;
                    mca.Doc = doc;
                    mca.Nit = Utilidades.Utilidades.NitEDS;
                    mca.Naturaleza = Utilidades.Utilidades.NatutalezaDebito;
                    mca.Cuenta = Utilidades.Utilidades.CuentaAjuste;
                    mca.Valor = diff1.ToString("0.00", CultureInfo.InvariantCulture);
                    mca.CentroCosto = "0901";
                    mca.CuentaBancaria = "";
                    movimientos.Add(mca);
                }
                else if (totalDebitos > totalCreditos)
                {
                    double diff = totalDebitos - totalCreditos;
                    MovimientoContableDTO mca = new MovimientoContableDTO();
                    mca.Fecha = fecha1.ToString("dd/MM/yyyy");
                    mca.TipoDoc = Utilidades.Utilidades.TipoMovimiento;
                    mca.Doc = doc;
                    mca.Nit = Utilidades.Utilidades.NitEDS;
                    mca.Naturaleza = Utilidades.Utilidades.NatutalezaCredito;
                    mca.Cuenta = Utilidades.Utilidades.CuentaAjuste;
                    mca.Valor = diff.ToString("0.00", CultureInfo.InvariantCulture);
                    mca.CentroCosto = "0901";
                    mca.CuentaBancaria = "";
                    movimientos.Add(mca);
                }
                #endregion
            }
            catch (EstacionDBException ex)
            {
                throw new PersistenciaException("Ha ocurrido un error al consultar los movimientos contables", ex);
            }

            return movimientos;
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
            catch (EstacionDBException ex)
            {
                throw new PersistenciaException("Error en la consulta clientes en DB app.", ex);
            }
        }

        public List<ClienteVO> consultarClientesServP()
        {
            try
            {
                return getClientesDAO().consultarClientesServP();
            }
            catch(EstacionDBException ex)
            {
                throw new PersistenciaException("Error en la consulta clientes en DB Servipunto.", ex);
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
            catch (EstacionDBException ex)
            {
                throw new PersistenciaException("Error en la actualizacion de cliente en DB app.", ex);
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

        public List<ProductoTurnoVO> consultarProductosTurno(DateTime fecha1, DateTime fecha2, long isla, long turno)
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
                return getProductosTurnoDAO().consultarProductosTurno(islas, turno, fecha1, fecha2);
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error en la consulta ventas en DB estación.", ex);
            }
        }

        public int guardarProductosTurno(List<ProductoTurnoVO> productosTurno)
        {
            try
            {
                return getProductosTurnoDAO().guardarProductosTurno(productosTurno);
            }
            catch (Exception ex)
            {
                throw new EstacionDBException("Error en la actualizacion de las ventas en DB app.", ex);
            }
        }

    }
}
