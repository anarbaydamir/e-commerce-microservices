﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Dal.Inter
{
    public interface IBasketContext
    {
        IDatabase Redis { get; }
    }
}
