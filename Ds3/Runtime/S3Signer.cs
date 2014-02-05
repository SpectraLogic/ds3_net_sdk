using System;

using System.Security.Cryptography;

namespace Ds3.Runtime
{
    class S3Signer
    {
        public static string Signature(string key, string payload)
        {
            Console.WriteLine(payload);
            HMACSHA1 hmac = new HMACSHA1(System.Text.Encoding.UTF8.GetBytes(key));
            byte[] hashResult = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(payload));
            return System.Convert.ToBase64String(hashResult).Trim();
        }
    }


}
