using Basket.API.Dal.Inter;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Dal.Impl
{
    public class BasketContext : IBasketContext
    {
        private readonly ConnectionMultiplexer connectionMultiplexer;

        public BasketContext(ConnectionMultiplexer connectionMultiplexer)
        {
            this.connectionMultiplexer = connectionMultiplexer;
            Redis = connectionMultiplexer.GetDatabase();
        }

        public IDatabase Redis { get; }
    }
}
