using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.DTO;
using System.IO;

namespace EstacionDB.Utilidades
{
    public class FileExporter
    {

        public const int EXCEL = 1;
        public const int PLANO = 2;

        public static bool exportar(List<MovimientoContableDTO> ventas, string ruta, int tipo)
        {
            try
            {
                FileStream fileStream = new FileStream(@ruta, FileMode.Create);

                TextWriter sw = new StreamWriter(fileStream);
                if (tipo == PLANO)
                {
                    if (Utilidades.usarEncabezados)
                    {
                        sw.WriteLine("FECHA\tTIPO\tDOC\tNIT\tCUENTA\tNAT\tVALOR\tCCOSTO");
                    }
                    foreach (MovimientoContableDTO linea in ventas)
                    {
                        sw.WriteLine(linea.Fecha + "\t" + linea.TipoDoc + "\t" + linea.Doc + "\t" + linea.Nit + "\t" + linea.Cuenta + "\t" + linea.Naturaleza + "\t" + linea.Valor + "\t" + linea.CentroCosto);
                    }
                }
                if (tipo == EXCEL)
                {
                    if (Utilidades.usarEncabezados)
                    {
                        sw.WriteLine("FECHA" + Utilidades.separador + "TIPO" + Utilidades.separador + "DOC" + Utilidades.separador + "NIT" + Utilidades.separador + "CUENTA" + Utilidades.separador + "NAT" + Utilidades.separador + "VALOR" + Utilidades.separador + "CCOSTO");
                    }
                    foreach (MovimientoContableDTO linea in ventas)
                    {
                        sw.WriteLine(linea.Fecha + Utilidades.separador + linea.TipoDoc + Utilidades.separador + linea.Doc + Utilidades.separador + linea.Nit + Utilidades.separador + linea.Cuenta + Utilidades.separador + linea.Naturaleza + Utilidades.separador + linea.Valor + Utilidades.separador + linea.CentroCosto);
                    }
                }

                sw.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
