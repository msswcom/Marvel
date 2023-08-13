using System.Security.Cryptography;
using System.Text;

namespace Marvel.Models.Extensions
{
    public static class StringExtension
    {
        public static string Md5(this string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes).ToLower();
            }
        }

        public static int ToInt(this string value)
        {
            int parsedValue;

            int.TryParse(value, out parsedValue);

            return parsedValue;
        }
    }
}
