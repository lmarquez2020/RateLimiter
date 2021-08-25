using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Lib.Model
{
    public class RateLimitRuleOptions
    {
        public string Resource { get; set; }
        public int Limit { get; set;  }
        public int PeriodInSeconds { get; set; }
    }
}
