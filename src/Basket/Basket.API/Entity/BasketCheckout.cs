using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entity
{
    public class BasketCheckout
    {
        public string userName { get; set; }
        public decimal totalPrice { get; set; }

        //billing address
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAdress { get; set; }
        public string addressLine { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }

        //payment
        public string cardName { get; set; }
        public string cardNumber { get; set; }
        public string expiration { get; set; }
        public string CVV { get; set; }
        public int paymentMethod { get; set; }
    }
}
