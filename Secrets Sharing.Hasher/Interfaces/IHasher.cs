using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secrets_Sharing.Hasher.Interfaces
{
    public interface IHasher
    {
        string GetHash(string key);
    }
}
