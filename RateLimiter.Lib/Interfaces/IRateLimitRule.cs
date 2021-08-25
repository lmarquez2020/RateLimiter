using RateLimiter.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Lib.Interfaces
{
    public interface IRateLimitRule
    {
        bool IsMatch(IEnumerable<ResourceData> data);
    }
}
