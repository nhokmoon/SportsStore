using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private Cart cart;

        public OrderRepository(ApplicationDbContext _context, Cart _cart)
        {
            context = _context;
            cart = _cart;
        }

        public IQueryable<Order> Orders => context.Orders;

        public void SaveOrder(Order order)
        {
            if (order.OrderId == 0)
            {
                order.Shipped = false;
                order.OrderPlaced = DateTime.Now;

                context.Orders.Add(order);
                context.SaveChanges();

                foreach (var item in cart.Items)
                {
                    var orderDetail = new OrderDetail()
                    {
                        Quantity = item.Quantity,
                        ProductId = item.Product.ProductId,
                        OrderId = order.OrderId,
                        Price = item.Product.Price
                    };

                    context.OrderDetails.Add(orderDetail);
                }

                context.SaveChanges();
            }
            else
            {
                context.Orders.FirstOrDefault(o => o.OrderId == order.OrderId).Shipped = order.Shipped;
                context.SaveChanges();
            }
        }
    }
}
