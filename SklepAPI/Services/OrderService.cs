using Microsoft.EntityFrameworkCore;
using SklepAPI.Entities;
using SklepAPI.Models;

namespace SklepAPI.Services
{
    public interface IOrderService
    {
        void NewOrder(NewOrderDto dto, int userId);
        void GetUserOrders(int userId);
    }

    public class OrderService : IOrderService
    {
        private readonly DatabaseContext _context;

        public OrderService(DatabaseContext context)
        {
            _context = context;
        }

        public void NewOrder(NewOrderDto dto, int userId)
        {
            var newOrder = new Order()
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                OrderStatus = 0, //0 - created
            };

            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            foreach(var product in dto.ListOfProductsId)
            {
                var productContext = _context
                    .Products
                    .First(r => r.Id == product.productId);

                var newOrderDetails = new OrderDetails()
                {
                    OrderId = newOrder.Id,
                    ProductId = product.productId,
                    Price = productContext.Price,
                    Quantity = product.quantity
                };

                productContext.Stock = productContext.Stock - product.quantity;
                _context.Products.Update(productContext);
                _context.OrdersDetails.Add(newOrderDetails);
            }

            _context.SaveChanges();
        }

        public void GetUserOrders(int userId)
        {
        
        }
    }
}
