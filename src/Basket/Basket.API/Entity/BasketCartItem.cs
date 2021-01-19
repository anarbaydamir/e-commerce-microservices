using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entity
{
    public class BasketCartItem
    {
        public int quantity { get; set; }
        public string color { get; set; }
        public decimal price { get; set; }
        public string productId { get; set; }
        public string productName { get; set; }
    }
}
