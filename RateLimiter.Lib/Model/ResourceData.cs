using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Lib.Model
{
    public class ResourceData
    {
        public string Token { get; set; }
        public string Location { get; set; }
        public string Resource { get; set; }
        public DateTime Created { get; set; }
    }
}
