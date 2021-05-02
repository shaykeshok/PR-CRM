using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BL.Modules
{
    class SHA
    {
        public string MD5Hash(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result;
            result = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i<=result.Length - 1; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }
           
    }
}
