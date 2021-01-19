using Basket.API.Dal.Inter;
using Basket.API.Entity;
using Basket.API.Repository.Inter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repository.Impl
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketContext context;

        public BasketRepository(IBasketContext context)
        {
            this.context = context;
        }

        public async Task<bool> delete(string userName)
        {
            return await context.Redis.KeyDeleteAsync(userName);
        }

        public async Task<BasketCart> get(string userName)
        {
            var basket = await context.Redis.StringGetAsync(userName);

            if (basket.IsNullOrEmpty)
                return null;

            //response from redis is json object
            return JsonConvert.DeserializeObject<BasketCart>(basket);
        }

        public async Task<BasketCart> update(BasketCart basket)
        {
            var updated = await context.Redis
                                    .StringSetAsync(basket.userName, JsonConvert.SerializeObject(basket));

            if (!updated)
                return null;

            return await get(basket.userName);
        }
    }
}
