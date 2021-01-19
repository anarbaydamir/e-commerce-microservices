using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entity
{
    public class BasketCart
    {
        public string userName { get; set; }
        public List<BasketCartItem> basketCartItemList { get; set; } = new List<BasketCartItem>();

        public BasketCart()
        {

        }

        public BasketCart(string userName)
        {
            this.userName = userName;
        }


        public decimal totalPrice
        {
            get
            {
                decimal price = 0;
                foreach(var item in basketCartItemList)
                {
                    price += item.quantity * item.price;
                }
                return price;
            }
        }
    }
}
