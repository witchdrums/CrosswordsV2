using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    public interface IEncryptionService
    {
    
        String SHA512_Encrypt(String stringInput);

    
    }
}
