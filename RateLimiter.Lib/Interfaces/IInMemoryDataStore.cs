using RateLimiter.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Lib.Interfaces
{
    public interface IInMemoryDataStore
    {
        IEnumerable<ResourceData> Get(string toke, string resource);
        void Save(string token, string location, string resource);
    }
}
