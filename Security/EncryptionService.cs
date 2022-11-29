using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    public class EncryptionService : IEncryptionService
    {
        public string StringToSHA512(string stringInput)
        {
            var stringBytes = System.Text.Encoding.UTF8.GetBytes(stringInput);

            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedStringBytes = hash.ComputeHash(stringBytes);
                var hashedStringOutputBuilder = new System.Text.StringBuilder(128);

                foreach (byte hashedStringByte in hashedStringBytes)
                {
                    hashedStringOutputBuilder.Append(hashedStringByte.ToString("X2"));
                }

                return hashedStringOutputBuilder.ToString();
            }
        }
    }
}
