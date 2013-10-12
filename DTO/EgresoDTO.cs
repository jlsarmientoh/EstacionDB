using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.DTO
{
    public class EgresoDTO
    {
        private long idEgreso;

        public long IdEgreso
        {
            get { return idEgreso; }
            set { idEgreso = value; }
        }
        private long tipoDocumento;

        public long TipoDocumento
        {
            get { return tipoDocumento; }
            set { tipoDocumento = value; }
        }
        private long numero;

        public  long Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        private string beneficiario;

        public  string Beneficiario
        {
            get { return beneficiario; }
            set { beneficiario = value; }
        }
        private double valor;

        public  double Valor
        {
            get { return valor; }
            set { valor = value; }
        }
        private DateTime fecha;

        public  DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        private DateTime fechaAplica;

        public  DateTime FechaAplica
        {
            get { return fechaAplica; }
            set { fechaAplica = value; }
        }
    }
}
