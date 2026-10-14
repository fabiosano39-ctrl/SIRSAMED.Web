using System.Security.Cryptography;
using System.Text;

namespace SIRSAMED.Web.Models
{
              // Clase auxiliar para la seguridad y cifrado del sistema
    public static class Seguridad
    {
               // Genera un hash criptografico SHA-256 a partir de una contrasena en texto plano (RNF-01)
        public static string GenerarHash(string textoPlano)
        {
            if (string.IsNullOrEmpty(textoPlano)) return string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                // Convierte la cadena de texto a un arreglo de bytes
                byte[] bytes = Encoding.UTF8.GetBytes(textoPlano);

                // Calcula el hash unidireccional
                byte[] hashBytes = sha256.ComputeHash(bytes);

                // Convierte los bytes en una cadena hexadecimal de 64 caracteres
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

                  // Valida si la contrasena ingresada coincide con el hash almacenado
        public static bool ValidarHash(string textoPlano, string hashAlmacenado)
        {
            string hashCalculado = GenerarHash(textoPlano);
            return StringComparer.OrdinalIgnoreCase.Compare(hashCalculado, hashAlmacenado) == 0;
        }
    }
}