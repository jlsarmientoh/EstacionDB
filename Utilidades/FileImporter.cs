using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.DTO;
using System.IO;
using System.Collections;

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
                        //e.FechaAplica = DateTime.Parse(columnas[2].Trim());
                        //Quinta columna
                        e.Valor = Utilidades.parsearDecimal(columnas[3].Trim(), ',');
                        egresos.Add(e);
                    }
                }
            }
            objReader.Close();

            foreach (EgresoDTO sOutput in egresos)
                Console.WriteLine(sOutput.ToString());
            Console.ReadLine();

            return egresos;
        }
    }
}