using System.Collections.Generic;

namespace Basket.API.Database.Entities
{
    public class BasketCart
    {
        public string UserName { get; set; }
        public List<BasketCartItem> Items { get; set; }

        public BasketCart()
        {
        }

        public BasketCart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice += item.Quantity * item.Price;
                }

                return totalPrice;
            }
        }
    }
}