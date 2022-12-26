using Secrets_Sharing.Hasher.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Secrets_Sharing.Hasher.Implementations
{
    public class MD5Hasher : IHasher
    {
        public string GetHash(string key)
        {
            MD5 md5 = MD5.Create();
            byte[] bytes= Encoding.UTF8.GetBytes(key);
            byte[] hash = md5.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();

            foreach(var b in hash)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
