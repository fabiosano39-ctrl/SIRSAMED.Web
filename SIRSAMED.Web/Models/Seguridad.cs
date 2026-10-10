using System.Security.Cryptography;
using System.Text;

namespace SIRSAMED.Web.Models
{
    public static class Seguridad
    {
        public static string GenerarHash(string textoPlano)
        {
            if (string.IsNullOrEmpty(textoPlano)) return string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(textoPlano);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool ValidarHash(string textoPlano, string hashAlmacenado)
        {
            string hashCalculado = GenerarHash(textoPlano);
            return StringComparer.OrdinalIgnoreCase.Compare(hashCalculado, hashAlmacenado) == 0;
        }
    }
}