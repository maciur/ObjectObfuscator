using ObjectObfuscator.Abstracts.Handlers;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ObjectObfuscator.Handlers
{
    public class StringValueHandler : IStringValueHandler
    {
        public string Obfuscate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            using var md5 = MD5.Create();
            var bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(value));
            var result = BitConverter.ToString(bytes);
            return result.Replace("-", "").ToLower();
        }
    }
}
