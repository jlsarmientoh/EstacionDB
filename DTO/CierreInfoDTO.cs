using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.VO;
using System.Collections;

namespace EstacionDB.DTO
{
    public class CierreInfoDTO
    {
        private CierreVentasVO cierreDia;

        public CierreVentasVO CierreDia
        {
            get { return cierreDia; }
            set { cierreDia = value; }
        }
        private double totalConsumo;

        public double TotalConsumo
        {
            get { return totalConsumo; }
            set { totalConsumo = value; }
        }
        private IList cierresTurnos;

        public IList CierresTurnos
        {
            get { return cierresTurnos; }
            set { cierresTurnos = value; }
        }


    }
}
