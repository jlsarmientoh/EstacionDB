using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.Security
{
    public static class Security
    {
        /// <summary>
        /// Retorna el hash SHA1 de la cadena de texto que recibe como parámetro.
        /// </summary>
        /// <param name="unHashed">Cadena de texto a encriptar.</param>
        /// <returns>Hash de la cadena de texto. SHA1.</returns>
        public static string createHash(string unHashed)
        {
            System.Security.Cryptography.SHA1CryptoServiceProvider x = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(unHashed);            
            data = x.ComputeHash(data);

            return Convert.ToBase64String(data);
        }

        /// <summary>
        /// Comprueba que dos cadenas tienen el mismo hash.
        /// </summary>
        /// <param name="hashData">Hash que se quiere comparar.  Por ejemplo el que está almacenado en la base de datos.</param>
        /// <param name="hashUser">Texto son encriptar, es convertido en hash y luego comparado con hashData.</param>
        /// <returns>true si los hashes coinciden, de lo contrario retorna false</returns>
        public static bool matchHash(string hashData, string hashUser)
        {
            hashUser = createHash(hashUser);
            if (hashUser == hashData)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
