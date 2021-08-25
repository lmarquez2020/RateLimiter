﻿using RateLimiter.Lib.Interfaces;
using RateLimiter.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Lib.Rules
{
    public class UsTokenRule : IRateLimitRule
    {
        readonly int _limitTimespanSeconds;
        readonly int _maxRequestCount;
        readonly List<ResourceData> _data = new List<ResourceData>();
        const string LOCATION = "USA";

        public UsTokenRule(RateLimitRuleOptions options)
        {
            _limitTimespanSeconds = options.PeriodInSeconds;
            _maxRequestCount = options.Limit;
        }
        public bool IsMatch(IEnumerable<ResourceData> data)
        {
            if (data.Last().Location != LOCATION) 
                return true;
            _data.Clear();
            _data.AddRange(data);

            if (_data.Count <= 1) return true;

            return _data.Count(i => i.Created >= DateTime.Now.AddSeconds(-_limitTimespanSeconds)) <= _maxRequestCount;
        }
    }
}
