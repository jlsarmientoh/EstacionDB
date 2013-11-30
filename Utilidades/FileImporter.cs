using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.DTO;
using System.IO;
using System.Collections;
using System.Globalization;

namespace EstacionDB.Utilidades
{
    public class FileImporter
    {
        public static List<EgresoDTO> importarEgresos(string ruta)
        {
            List<EgresoDTO> egresos = new List<EgresoDTO>();

            StreamReader objReader = new StreamReader(ruta);
            string sLine = "";
            ArrayList arrText = new ArrayList();

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                {
                    string[] columnas = sLine.Split(';');
                    if (columnas[0].Trim().Equals("CE"))
                    {
                        EgresoDTO e = new EgresoDTO();
                        //Pimera columna
                        e.TipoDocumento = 1;
                        //Segunda columna
                        e.Numero = long.Parse(columnas[1].Trim());
                        //Tercera columna                        
                        e.FechaAplica = DateTime.ParseExact(columnas[2].Trim(), "yyyy/dd/MM", CultureInfo.InvariantCulture);
                        //Quinta columna
                        e.Valor = Utilidades.parsearDecimal(columnas[6].Trim(), ',');
                        egresos.Add(e);
                    }
                }
            }
            objReader.Close();

            return egresos;
        }
    }
}