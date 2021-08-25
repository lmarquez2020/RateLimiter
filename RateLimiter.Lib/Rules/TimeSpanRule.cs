using RateLimiter.Lib.Interfaces;
using RateLimiter.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Lib.Rules
{
    public class TimeSpanRule : IRateLimitRule
    {
        readonly int _limitTimespanSeconds;
        readonly List<ResourceData> _data = new List<ResourceData>();

        public TimeSpanRule(RateLimitRuleOptions options)
        {
            _limitTimespanSeconds = options.PeriodInSeconds;
        }
        public bool IsMatch(IEnumerable<ResourceData> data)
        {
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
