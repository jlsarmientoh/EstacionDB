using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.VO
{
    public class EgresoVO
    {
        private long idEgreso = 0;

        public virtual long IdEgreso
        {
            get { return idEgreso; }
            set { idEgreso = value; }
        }
        private long documento = 1;

        public virtual long Documento
        {
            get { return documento; }
            set { documento = value; }
        }
        private long numero;

        public virtual long Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        private string beneficiario;

        public virtual string Beneficiario
        {
            get { return beneficiario; }
            set { beneficiario = value; }
        }
        private double valor;

        public virtual double Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        private DateTime fecha;

        public virtual DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        private DateTime fechaAplica;

        public virtual DateTime FechaAplica
        {
            get { return fechaAplica; }
            set { fechaAplica = value; }
        }

        private int isla;

        public virtual int Isla
        {
            get { return isla; }
            set { isla = value; }
        }
    }
}
