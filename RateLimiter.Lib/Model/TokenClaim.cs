﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Lib.Model
{
    public class TokenClaim
    {
        public string Token { get; set; }
        public string Location { get; set; }
    }
}
