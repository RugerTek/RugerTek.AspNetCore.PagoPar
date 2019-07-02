using System;
using System.Security.Cryptography;
using System.Text;

namespace RugerTek.AspNetCore.PagoPar.Helpers
{
    public static class EncyptionHelpers
    {
        public static string Sha1(this string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
