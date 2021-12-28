using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Cart
    {
        private List<CartItem> cartItems = new List<CartItem>();

        public virtual void AddItem(Product product, int quantity)
        {
            CartItem item = cartItems.Where(p => p.Product.ProductId == product.ProductId).FirstOrDefault();

            if (item == null)
            {
                cartItems.Add(new CartItem
                {
                    Product = product, 
                    Quantity = quantity
                });;
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public virtual void RemoveItem(Product product)
        {
            cartItems.RemoveAll(p => p.Product.ProductId == product.ProductId);
        }

        public virtual decimal ComputeTotalValue()
        {
            return cartItems.Sum(p => (decimal) p.Product.Price * p.Quantity);
        }

        public virtual void Clear()
        {
            cartItems.Clear();
        }

        public virtual IEnumerable<CartItem> Items => cartItems;
    }
}
