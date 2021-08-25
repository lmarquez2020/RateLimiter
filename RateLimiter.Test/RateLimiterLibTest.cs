using System;
using Xunit;
using System.Collections.Generic;
using System.Collections;
using RateLimiter.Lib.Interfaces;
using RateLimiter.Lib.Persistence;
using System.Linq;
using RateLimiter.Lib.Rules;
using RateLimiter.Lib.Model;
using System.Threading;

namespace RateLimiterLib.Test
{
    public class RateLimiterLibTest
    {
        IRateLimiter _rateLimiter;

        public RateLimiterLibTest()
        {
            
        }

        [Fact]
        public void Allow3SecondsTimeSpanTest()
        {
            // Arrange
            var configuration = new RateLimitRuleOptions
            {
                Resource = "*",
                Limit = 1,
                PeriodInSeconds = 5
            };
            var limiter = new RateLimiter.Lib.RateLimiter(new InMemoryDataStore(), configuration, new[] { new TimeSpanRule(configuration) });
            _rateLimiter = limiter;

            var token = new TokenClaim
            {
                Token = "12345",
                Location = "USA"
            };

            // Act
            var result1 = _rateLimiter.CheckAccess(token);
            var result2 = _rateLimiter.CheckAccess(token);

            // Assert
            Assert.True(result2);
        }
        [Fact]
        public void NotAllow4SecondsTimeSpanTest()
        {
            // Arrange
            var configuration = new RateLimitRuleOptions
            {
                Resource = "*",
                Limit = 1,
                PeriodInSeconds = 3
            };
            var limiter = new RateLimiter.Lib.RateLimiter(new InMemoryDataStore(), configuration, new[] { new TimeSpanRule(configuration)});
            _rateLimiter = limiter;
            var token = new TokenClaim
            {
                Token = "12345",
                Location = "USA"
            };

            // Act
            var result1 = _rateLimiter.CheckAccess(token);
            Thread.Sleep(4000);
            var result2 = _rateLimiter.CheckAccess(token);

            // Assert
            Assert.False(result2);
        }
        [Fact]
        public void Allow3MaxRequestsTest()
        {
            // Arrange
            var configuration = new RateLimitRuleOptions
            {
                Resource = "*",
                Limit = 3,
                PeriodInSeconds = 3
            };
            var limiter = new RateLimiter.Lib.RateLimiter(new InMemoryDataStore(), configuration, new[] { new MaxRequestRule(configuration) });
            _rateLimiter = limiter;

            var token = new TokenClaim
            {
                Token = "123457",
                Location = "USA"
            };

            // Act
            var result1 = _rateLimiter.CheckAccess(token);
            var result2 = _rateLimiter.CheckAccess(token);
            var result3 = _rateLimiter.CheckAccess(token);

            // Assert
            Assert.True(result3);
        }
        [Fact]
        public void NotAllow4RequestFrom3MaxRequestsTest()
        {
            // Arrange
            var configuration = new RateLimitRuleOptions
            {
                Resource = "*",
                Limit = 3,
                PeriodInSeconds = 3
            };
            var limiter = new RateLimiter.Lib.RateLimiter(new InMemoryDataStore(), configuration, new[] { new MaxRequestRule(configuration) });
            _rateLimiter = limiter;

            var token = new TokenClaim
            {
                Token = "123458",
                Location = "USA"
            };

            // Act
            var result1 = _rateLimiter.CheckAccess(token);
            var result2 = _rateLimiter.CheckAccess(token);
            var result3 = _rateLimiter.CheckAccess(token);
            var result4 = _rateLimiter.CheckAccess(token);

            // Assert
            Assert.False(result4);
        }
        [Fact]
        public void AllowUSA3MaxRequestsTest()
        {
            // Arrange
            var configuration = new RateLimitRuleOptions
            {
                Resource = "*",
                Limit = 3,
                PeriodInSeconds = 3
            };
            var limiter = new RateLimiter.Lib.RateLimiter(new InMemoryDataStore(), configuration, new[] { new UsTokenRule(configuration) });
            _rateLimiter = limiter;

            var token = new TokenClaim
            {
                Token = "123457",
                Location = "USA"
            };

            // Act
            var result1 = _rateLimiter.CheckAccess(token);
            var result2 = _rateLimiter.CheckAccess(token);
            var result3 = _rateLimiter.CheckAccess(token);

            // Assert
            Assert.True(result3);
        }
        [Fact]
        public void NotAllowUSA4RequestFrom3MaxRequestsTest()
        {
            // Arrange
            var configuration = new RateLimitRuleOptions
            {
                Resource = "*",
                Limit = 3,
                PeriodInSeconds = 3
            };
            var limiter = new RateLimiter.Lib.RateLimiter(new InMemoryDataStore(), configuration, new[] { new UsTokenRule(configuration) });
            _rateLimiter = limiter;

            var token = new TokenClaim
            {
                Token = "123458",
                Location = "USA"
            };

            // Act
            var result1 = _rateLimiter.CheckAccess(token);
            var result2 = _rateLimiter.CheckAccess(token);
            var result3 = _rateLimiter.CheckAccess(token);
            var result4 = _rateLimiter.CheckAccess(token);

            // Assert
            Assert.False(result4);
        }
        [Fact]
        public void AllowEU3SecondsTimeSpanTest()
        {
            // Arrange
            var configuration = new RateLimitRuleOptions
            {
                Resource = "*",
                Limit = 1,
                PeriodInSeconds = 3
            };
            var limiter = new RateLimiter.Lib.RateLimiter(new InMemoryDataStore(), configuration, new[] { new EuTokenRule(configuration) });
            _rateLimiter = limiter;

            var token = new TokenClaim
            {
                Token = "123450",
                Location = "EU"
            };

            // Act
            var result1 = _rateLimiter.CheckAccess(token);
            var result2 = _rateLimiter.CheckAccess(token);

            // Assert
            Assert.True(result2);
        }
        [Fact]
        public void NotAllowEU4SecondsTimeSpanTest()
        {
            // Arrange
            var configuration = new RateLimitRuleOptions
            {
                Resource = "*",
                Limit = 1,
                PeriodInSeconds = 3
            };
            var limiter = new RateLimiter.Lib.RateLimiter(new InMemoryDataStore(), configuration, new[] { new EuTokenRule(configuration) });
            _rateLimiter = limiter;
            var token = new TokenClaim
            {
                Token = "123450",
                Location = "EU"
            };

            // Act
            var result1 = _rateLimiter.CheckAccess(token);
            Thread.Sleep(4000);
            var result2 = _rateLimiter.CheckAccess(token);

            // Assert
            Assert.False(result2);
        }
        [Fact]
        public void Allow10MaxRequestIn10SecondsTimeSpanTest()
        {
            // Arrange
            var configuration = new RateLimitRuleOptions
            {
                Resource = "*",
                Limit = 10,
                PeriodInSeconds = 60
            };

            var rules = new List<IRateLimitRule>();
            rules.Add(new MaxRequestRule(configuration));
            rules.Add(new TimeSpanRule(configuration));
            var limiter = new RateLimiter.Lib.RateLimiter(new InMemoryDataStore(), configuration, rules);
            _rateLimiter = limiter;

            var token = new TokenClaim
            {
                Token = "223450",
                Location = "USA"
            };

            // Act
            var result1 = _rateLimiter.CheckAccess(token);
            var result2 = _rateLimiter.CheckAccess(token);
            var result3 = _rateLimiter.CheckAccess(token);
            var result4 = _rateLimiter.CheckAccess(token);
            var result5 = _rateLimiter.CheckAccess(token);
            var result6 = _rateLimiter.CheckAccess(token);
            var result7 = _rateLimiter.CheckAccess(token);
            var result8 = _rateLimiter.CheckAccess(token);
            var result9 = _rateLimiter.CheckAccess(token);
            var result10 = _rateLimiter.CheckAccess(token);

            // Assert
            Assert.True(result10);
        }
    }
}
