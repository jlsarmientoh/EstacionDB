using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.VO;
using EstacionDB.DTO;
using EstacionDB.DAO;
using EstacionDB.Exceptions;

namespace EstacionDB.Core
{
    public class ControlTanquesCore
    {
        //Atributos
        private volatile static ControlTanquesCore instance;
        private ControlCombustibleDAO controlCombustibleDAO;
        private LecturasDAO lecturasDAO;
        private CompraCombustibleDAO comprasCombustibleDAO;
        //Constructor
        private ControlTanquesCore()
        {
            controlCombustibleDAO = new ControlCombustibleDAO();
            lecturasDAO = new LecturasDAO();
            comprasCombustibleDAO = new CompraCombustibleDAO();
        }

        public static ControlTanquesCore getInstance()
        {
            if (instance == null)
            {
                instance = new ControlTanquesCore();
            }
            return instance;
        }

        public int procesarControles(DateTime fecha, LecturaDTO[] lecturas)
        {
            int rows = 0;
            bool isCorriente = false;
            bool isSuper = false;
            bool isDiesel = false;
            ControlCombustibleVO controlCorriente = null;
            ControlCombustibleVO controlSuper = null;
            ControlCombustibleVO controlDiesel = null;

            try
            {

                for (int i = 0; i < lecturas.Length; i++)
                {
                    int idProd = lecturas[i].IdProducto;
                    switch (idProd)
                    {
                        //Corriente
                        case 1:
                            {
                                controlCorriente = existeControl(idProd, fecha);                                
                                controlCorriente.InventarioFinal += (int)lecturas[i].Galones;
                                isCorriente = true;
                                break;
                            }
                        //Super
                        case 2:
                            {
                                controlSuper = existeControl(idProd, fecha);
                                controlSuper.InventarioFinal += (int)lecturas[i].Galones;
                                isSuper = true;
                                break;
                            }
                        //Diesel
                        case 3:
                            {
                                controlDiesel = existeControl(idProd, fecha);
                                controlDiesel.InventarioFinal += (int)lecturas[i].Galones;
                                isDiesel = true;
                                break;
                            }
                    }
                }
                DateTime diaAnterior = fecha.AddDays(-1);
                //Corriente
                if (isCorriente)
                {
                    controlCorriente.IdProducto = 1;
                    controlCorriente.Fecha = fecha;
                    controlCorriente.GalonesCompra = (int)this.consultarComprasGalonesFechaProducto(1, fecha);
                    controlCorriente.InventarioInicial = (int)this.consultarSaldoAnterior(1, diaAnterior, diaAnterior);
                    controlCorriente.VentaMedida = (controlCorriente.GalonesCompra + controlCorriente.InventarioInicial) - controlCorriente.InventarioFinal;
                    controlCorriente.VentaSurtidor = (int)this.consultarVentaProducto(fecha, fecha, 1);
                    controlCorriente.SobranteDia = controlCorriente.VentaSurtidor - controlCorriente.VentaMedida;
                    controlCorriente.SobranteAcumulado = controlCorriente.SobranteDia + (int)this.consultarAcumuladoAnterior(1, diaAnterior, diaAnterior);
                    controlCorriente.Procentaje = (double)(((double)controlCorriente.SobranteDia / (double)(controlCorriente.GalonesCompra + controlCorriente.InventarioInicial)) * 100);

                    rows += this.guardarControlCombustible(controlCorriente);
                }

                //Super
                if (isSuper)
                {
                    controlSuper.IdProducto = 2;
                    controlSuper.Fecha = fecha;
                    controlSuper.GalonesCompra = (int)this.consultarComprasGalonesFechaProducto(2, fecha);
                    controlSuper.InventarioInicial = (int)this.consultarSaldoAnterior(2, diaAnterior, diaAnterior);
                    controlSuper.VentaMedida = (controlSuper.GalonesCompra + controlSuper.InventarioInicial) - controlSuper.InventarioFinal;
                    controlSuper.VentaSurtidor = (int)this.consultarVentaProducto(fecha, fecha, 2);
                    controlSuper.SobranteDia = controlSuper.VentaSurtidor - controlSuper.VentaMedida;
                    controlSuper.SobranteAcumulado = controlSuper.SobranteDia + (int)this.consultarAcumuladoAnterior(2, diaAnterior, diaAnterior);
                    controlSuper.Procentaje = (double)(((double)controlSuper.SobranteDia / (double)(controlSuper.GalonesCompra + controlSuper.InventarioInicial)) * 100);

                    rows += this.guardarControlCombustible(controlSuper);
                }

                //Diesel
                if (isDiesel)
                {
                    controlDiesel.IdProducto = 3;
                    controlDiesel.Fecha = fecha;
                    controlDiesel.GalonesCompra = (int)this.consultarComprasGalonesFechaProducto(3, fecha);
                    controlDiesel.InventarioInicial = (int)this.consultarSaldoAnterior(3, diaAnterior, diaAnterior);
                    controlDiesel.VentaMedida = (controlDiesel.GalonesCompra + controlDiesel.InventarioInicial) - controlDiesel.InventarioFinal;
                    controlDiesel.VentaSurtidor = (int)this.consultarVentaProducto(fecha, fecha, 3);
                    controlDiesel.SobranteDia = controlDiesel.VentaSurtidor - controlDiesel.VentaMedida;
                    controlDiesel.SobranteAcumulado = controlDiesel.SobranteDia + (int)this.consultarAcumuladoAnterior(3, diaAnterior, diaAnterior);
                    controlDiesel.Procentaje = (double)(((double)controlDiesel.SobranteDia / (double)(controlDiesel.GalonesCompra + controlDiesel.InventarioInicial)) * 100);

                    rows += this.guardarControlCombustible(controlDiesel);
                }
            }
            catch (PersistenciaException ex)
            {
                throw new ControlCombustibleException("Error al procesar las lecturas", ex);
            }
            return rows;
        }

        public void reProcesarControlesProducto(int idProducto, DateTime fechaInicio, DateTime fechaFin)
        {   
            try
            {
                List<ControlCombustibleVO> controles = controlCombustibleDAO.consultarControles(idProducto, fechaInicio, fechaFin);

                foreach (ControlCombustibleVO cc in controles)
                {
                    DateTime diaAnterior = cc.Fecha.AddDays(-1);
                    
                    cc.GalonesCompra = (int)this.consultarComprasGalonesFechaProducto(idProducto, cc.Fecha);
                    cc.InventarioInicial = (int)this.consultarSaldoAnterior(idProducto, diaAnterior, diaAnterior);
                    cc.VentaMedida = (cc.GalonesCompra + cc.InventarioInicial) - cc.InventarioFinal;
                    cc.VentaSurtidor = (int)this.consultarVentaProducto(cc.Fecha, cc.Fecha, idProducto);
                    cc.SobranteDia = cc.VentaSurtidor - cc.VentaMedida;
                    cc.SobranteAcumulado = cc.SobranteDia + (int)this.consultarAcumuladoAnterior(idProducto, diaAnterior, diaAnterior);
                    cc.Procentaje = (double)(((double)cc.SobranteDia / (double)(cc.GalonesCompra + cc.InventarioInicial)) * 100);

                    this.guardarControlCombustible(cc);
                }
            }
            catch (PersistenciaException ex)
            {
                throw new ControlCombustibleException("Error al re-procesar las lecturas", ex);
            }
        }

        /// <summary>
        /// <para>Guarda o actualiza el control de combustible.</para>
        /// </summary>
        /// <param name="cc">Objeto VO con los datos del control de combustible a guardar o actualizar</param>
        /// <returns># de filas afectadas</returns>
        public int guardarControlCombustible(ControlCombustibleVO cc)
        {
            try
            {
                return controlCombustibleDAO.guardarControlCombustible(cc);
            }
            catch (EstacionDBException ex)
            {
                throw new ControlCombustibleException("Error al guardar el control de combustible", ex);
            }
        }

        /// <summary>
        /// <para>Retorna la lista de controles de combustible para el producto y el rango de fechas recibido como parámetro.</para>
        /// </summary>
        /// <param name="idProducto">Código del producto.</param>
        /// <param name="fechadesde">Fecha de inicio.</param>
        /// <param name="fechaHasta">Fecha final.</param>
        /// <returns>Lista de controles de combustible. Objetos VO</returns>
        public List<ControlCombustibleVO> consultarControlProductoFecha(int idProducto, DateTime fechadesde, DateTime fechaHasta)
        {
            try
            {
                return controlCombustibleDAO.consultarControles(idProducto, fechadesde, fechaHasta);
            }
            catch (EstacionDBException ex)
            {
                throw new ControlCombustibleException("Error al consultar el control de combustible por fecha y producto", ex);
            }
        }

        /// <summary>
        /// <para>Retorna el saldo de la fecha recibida como parámetro</para>
        /// </summary>
        /// <param name="idProducto">Código del producto.</param>
        /// <param name="fechadesde">Fecha de inicio.</param>
        /// <param name="fechaHasta">Fecha final.</param>
        /// <returns>Valor del saldo anterior o inventario final.</returns>
        private double consultarSaldoAnterior(int idProducto, DateTime fechadesde, DateTime fechaHasta)
        {
            try
            {
                double saldo = 0;
                List<ControlCombustibleVO> lista = controlCombustibleDAO.consultarControles(idProducto, fechadesde, fechaHasta);

                if (lista != null && lista.Count > 0)
                {
                    ControlCombustibleVO control = lista[0];
                    saldo = control.InventarioFinal;
                }
                return saldo;
            }
            catch (EstacionDBException ex)
            {
                throw new PersistenciaException("Error al consultar el control de combustible por fecha y producto", ex);
            }
        }

        /// <summary>
        /// <para>Retorna el acumulado de la fecha recibida como parámetro</para>
        /// </summary>
        /// <param name="idProducto">Código del producto.</param>
        /// <param name="fechadesde">Fecha de inicio.</param>
        /// <param name="fechaHasta">Fecha final.</param>
        /// <returns>Valor del acumulado.</returns>
        private double consultarAcumuladoAnterior(int idProducto, DateTime fechadesde, DateTime fechaHasta)
        {
            try
            {
                double acumulado = 0;
                if (fechadesde.Day > 1)
                {
                    List<ControlCombustibleVO> lista = controlCombustibleDAO.consultarControles(idProducto, fechadesde, fechaHasta);

                    if (lista != null && lista.Count > 0)
                    {
                        ControlCombustibleVO control = lista[0];
                        acumulado = control.SobranteAcumulado;
                    }
                }
                return acumulado;
            }
            catch (EstacionDBException ex)
            {
                throw new PersistenciaException("Error al consultar el control de combustible por fecha y producto", ex);
            }
        }

        /// <summary>
        /// <para>Consulta el valor de las compras de combustible para el tipo de producto y la fecha recibidos como parámetro.</para> 
        /// </summary>
        /// <param name="idProducto">Código del producto.</param>
        /// <param name="fecha">Fecha a consultar.</param>
        /// <returns>El valor de las compras de combustible.</returns>
        private double consultarComprasGalonesFechaProducto(long idProducto, DateTime fecha)
        {
            try
            {
                return comprasCombustibleDAO.consultarComprasGalonesFechaProducto(idProducto, fecha, fecha);
            }
            catch (EstacionDBException ex)
            {
                throw new PersistenciaException("Error al consultar las compras de combustible", ex);
            }
        }

        /// <summary>
        /// <para>Retorna el total de galones vendidos para el tipo de producto y el rango de fechas recibidos como parámetros.</para>
        /// </summary>
        /// <param name="fecha1">Fecha de inicio.</param>
        /// <param name="fecha2">Fecha final.</param>
        /// <param name="idProdcuto">Código de producto.</param>
        /// <returns>La cantidad de galones.</returns>
        public double consultarVentaProducto(DateTime fecha1, DateTime fecha2, int idProdcuto)
        {
            try
            {
                double diferencia = 0;
                int prod = 0;
                switch (idProdcuto)
                {
                    case 1:
                        {
                            prod = Utilidades.Utilidades.codigoCorriente;
                            break;
                        }
                    case 2:
                        {
                            prod = Utilidades.Utilidades.codigoSuper;
                            break;
                        }
                    case 3:
                        {
                            prod = Utilidades.Utilidades.codigoDiesel;
                            break;
                        }
                }
                diferencia = lecturasDAO.consultarDiferenciaProducto(fecha1, fecha2, prod);
                diferencia = Math.Round(diferencia, 0, MidpointRounding.AwayFromZero);

                return diferencia;
            }
            catch (EstacionDBException ex)
            {
                throw new PersistenciaException("Error en la consulta lecturas en DB estación.", ex);
            }
        }

        /// <summary>
        /// <para>Consulta si ya existe un control de combustible registrado para el tipo de producto y fecha recibidos como parámetro</para>
        /// </summary>
        /// <param name="idProducto">Código del producto.</param>
        /// <param name="fecha">Fecha a consultar.</param>
        /// <returns>El control de combustible existente o un objeto VO vacio en caso de que no exista.</returns>
        private ControlCombustibleVO existeControl(int idProducto, DateTime fecha)
        {
            try
            {
                return controlCombustibleDAO.consultarControl(idProducto, fecha);
            }
            catch (EstacionDBException ex)
            {
                return new ControlCombustibleVO();
            }
        }
    }
}
