using Basket.API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repository.Inter
{
    public interface IBasketRepository
    {
        Task<BasketCart> get(string userName);
        Task<BasketCart> update(BasketCart basket);
        Task<bool> delete(string userName);
    }
}
