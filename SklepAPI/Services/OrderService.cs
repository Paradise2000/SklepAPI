using Microsoft.EntityFrameworkCore;
using SklepAPI.Entities;
using SklepAPI.Exceptions;
using SklepAPI.Models;
using SklepAPI.Tracking;
using SklepAPI.Tracking.TrackingModel;

namespace SklepAPI.Services
{
    public interface IOrderService
    {
        void NewOrder(NewOrderDto dto, int userId);
        IEnumerable<GetUserOrdersDto> GetUsersOrders();
        IEnumerable<GetUserOrdersDto> GetLoggedUserOrders(int userId);
        void ChangeOrderStatus(int orderId ,OrderStatus status);
        TrackingDto GetTrackingInfo(string TrackingNumber);
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
                OrderStatus = OrderStatus.Created, //0 - created
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
                    Price = productContext.Price * product.quantity,
                    Quantity = product.quantity
                };

                productContext.Stock = productContext.Stock - product.quantity;
                _context.Products.Update(productContext);
                _context.OrdersDetails.Add(newOrderDetails);
            }

            _context.SaveChanges();
        }

        public IEnumerable<GetUserOrdersDto> GetUsersOrders()
        {
            List<GetUserOrdersDto> ListOfOrders = new List<GetUserOrdersDto>();

            var Orders = _context
                .Orders
                .OrderBy(r => r.OrderDate)
                .ToList();

            foreach (var order in Orders)
            {
                var orderDto = new GetUserOrdersDto()
                {
                    OrderId = order.Id,
                    UserId = order.UserId,
                    OrderStatus = order.OrderStatus.ToString()
                };

                var Products = (
                    from p in _context.Products
                    join d in _context.OrdersDetails on p.Id equals d.ProductId
                    where (d.OrderId == order.Id)
                    select new ProductsGetUserOrdersDto
                    {
                        ProductId = p.Id,
                        ProductName = p.Name,
                        ProductPrice = p.Price,
                        Quantity = d.Quantity
                    }).ToList();

                orderDto.Products = Products;
                ListOfOrders.Add(orderDto);
            }

            return ListOfOrders;
        }

        public IEnumerable<GetUserOrdersDto> GetLoggedUserOrders(int userId)
        {
            List<GetUserOrdersDto> ListOfOrders = new List<GetUserOrdersDto>();

            var Orders = _context
                .Orders
                .OrderBy(r => r.OrderDate)
                .Where(r => r.UserId == userId)
                .ToList();

            foreach (var order in Orders)
            {
                var orderDto = new GetUserOrdersDto()
                {
                    OrderId = order.Id,
                    UserId = order.UserId,
                    OrderStatus = order.OrderStatus.ToString()
                };

                var Products = (
                    from p in _context.Products
                    join d in _context.OrdersDetails on p.Id equals d.ProductId
                    where (d.OrderId == order.Id)
                    select new ProductsGetUserOrdersDto
                    {
                        ProductId = p.Id,
                        ProductName = p.Name,
                        ProductPrice = p.Price,
                        Quantity = d.Quantity
                    }).ToList();

                orderDto.Products = Products;
                ListOfOrders.Add(orderDto);
            }

            return ListOfOrders;
        }

        public void ChangeOrderStatus(int orderId, OrderStatus status)
        {
            var Order = _context
                .Orders
                .FirstOrDefault(r => r.Id == orderId);

            if(Order == null)
            {
                throw new NotFoundException("Order not found");
            }

            Order.OrderStatus = status;

            _context.SaveChanges();
        }

        public TrackingDto GetTrackingInfo(string TrackingNumber)
        {
            TrackingHistory tracking = new TrackingHistory();
            var result = tracking.CheckTrackingHistory(TrackingNumber).Result;

            if(result == null)
            {
                throw new NotFoundException("Delivery company not found, or tracking is not available");
            }

            return result;
        }

    }
}
