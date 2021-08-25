using RateLimiter.Lib.Interfaces;
using RateLimiter.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Lib
{
    public class RateLimiterRuleEngine
    {
        protected readonly IInMemoryDataStore _store;
        RateLimitRuleOptions _rateLimitRules;
        List<IRateLimitRule> _rules = new List<IRateLimitRule>();

        public RateLimiterRuleEngine(IInMemoryDataStore store, IEnumerable<IRateLimitRule> rules, RateLimitRuleOptions rateLimitRule)
        {
            _store = store;
            _rules.AddRange(rules);
            _rateLimitRules = rateLimitRule;
        }
        public bool IsAllowed(TokenClaim claim)
         {
            if (_rules == null) return true;

            _store.Save(claim.Token, claim.Location,_rateLimitRules.Resource);
            var result = _store.Get(claim.Token, _rateLimitRules.Resource);

            foreach(var rule in _rules)
            {
                if (!rule.IsMatch(result))
                    return false;
            }

            return true;
        }
    }
}
