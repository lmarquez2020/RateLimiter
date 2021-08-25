using RateLimiter.Lib.Interfaces;
using RateLimiter.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Lib.Persistence
{
    public class InMemoryDataStore : IInMemoryDataStore
    {
        private readonly List<ResourceData> _cache = new List<ResourceData>();
        public IEnumerable<ResourceData> Get(string token, string resource)
        {
            return _cache.Where(i => i.Token == token && i.Resource == resource);
        }

        public void Save(string token, string location, string resource)
        {
            _cache.Add(new ResourceData
            {
                Token = token,
                Resource = resource,
                Location = location,
                Created = DateTime.UtcNow
            });
        }
    }
}
