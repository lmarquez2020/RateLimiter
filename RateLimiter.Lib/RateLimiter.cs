using RateLimiter.Lib.Interfaces;
using RateLimiter.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Lib
{
    public class RateLimiter : IRateLimiter
    {
        readonly IInMemoryDataStore _dataStore;
        readonly RateLimitRuleOptions _configuration;
        readonly IEnumerable<IRateLimitRule> _rules;
        public RateLimiter(IInMemoryDataStore store, RateLimitRuleOptions options)
        {
            _dataStore = store;
            _configuration = options;

            var ruleType = typeof(IRateLimitRule);
            IEnumerable<IRateLimitRule> _rules = this.GetType().Assembly.GetTypes()
                .Where(p => ruleType.IsAssignableFrom(p) && !p.IsInterface)
                .Select(t => Activator.CreateInstance(t, _configuration) as IRateLimitRule);
        }
        public RateLimiter(IInMemoryDataStore store, RateLimitRuleOptions options, IEnumerable<IRateLimitRule> rules)
        {
            _dataStore = store;
            _configuration = options;
            _rules = rules;
        }
        public bool CheckAccess(TokenClaim token)
        {
            var engine = new RateLimiterRuleEngine(_dataStore, _rules, _configuration);
            return engine.IsAllowed(token);
        }
    }
}
