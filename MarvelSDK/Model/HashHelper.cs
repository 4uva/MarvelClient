using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MarvelSDK.Model
{
    class HashHelper
    {
        MD5 md5 = MD5.Create();
        string publicKey, privateKey;

        public HashHelper(string privateKey, string publicKey)
        {
            this.publicKey = publicKey;
            this.privateKey = privateKey;
        }

        public string ComputeHash(int ts)
        {
            string input = ts + privateKey + publicKey;
            // taken from https://devblogs.microsoft.com/csharpfaq/how-do-i-calculate-a-md5-hash-from-a-string/
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
