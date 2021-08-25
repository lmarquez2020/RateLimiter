using RateLimiter.Lib.Interfaces;
using RateLimiter.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Lib.Rules
{
    public class EuTokenRule : IRateLimitRule
    {
        readonly int _limitTimespanSeconds;
        readonly List<ResourceData> _data = new List<ResourceData>();
        const string LOCATION = "EU";

        public EuTokenRule(RateLimitRuleOptions options)
        {
            _limitTimespanSeconds = options.PeriodInSeconds;
        }
        public bool IsMatch(IEnumerable<ResourceData> data)
        {
            if (data.Last().Location != LOCATION) 
                return true;
            _data.Clear();
            _data.AddRange(data);

            var lastDataItem = _data.Last();
            var currentTime = lastDataItem.Created;
            if (_data.Count <= 1) return true;

            var prevTime = _data[_data.Count - 2];

            return currentTime.Subtract(prevTime.Created) <= new TimeSpan(0, 0, _limitTimespanSeconds);
        }
    }
}
