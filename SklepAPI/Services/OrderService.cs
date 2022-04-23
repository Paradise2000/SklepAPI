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
        void AddDeliveryOption(DeliveryOptionDto dto);
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
            var Order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                OrderStatus = OrderStatus.Created,
                DeliveryOptionId = dto.DeliveryOptionId,
                OrdersDetails = dto.ListOfProductsId.Select(p => new OrderDetails
                {
                    ProductId = p.productId,
                    Price = _context.Products.First(w => w.Id == p.productId).Price * p.quantity,
                    Quantity = p.quantity,
                    TrackingNumber = "-"
                }).ToList()
            };

            foreach(var UpdateElement in dto.ListOfProductsId)
            {
                var UpdateStock = _context.Products.First(p => p.Id == UpdateElement.productId);
                UpdateStock.Stock -= UpdateElement.quantity;
                if(UpdateStock.Stock < 0)
                {
                    throw new BadRequestException($"item with id {UpdateElement.productId} is out of stock");
                }
            }

            _context.Orders.Add(Order);
            _context.SaveChanges();
        }

        public void AddDeliveryOption(DeliveryOptionDto dto)
        {
            var delivery = new DeliveryOption
            {
                DeliveryType = dto.DeliveryType,
                PricePerDelivery = dto.PricePerDelivery
            };

            _context.DeliveryOptions.Add(delivery);
            _context.SaveChanges();
        }

        public IEnumerable<GetUserOrdersDto> GetUsersOrders()
        {
            var ListOfOrders =
                _context.Orders
                .Select(o => new GetUserOrdersDto
                {
                    OrderId = o.Id,
                    UserId = o.UserId,
                    OrderStatus = o.OrderStatus.ToString(),
                    DeliveryType = o.DeliveryOption.DeliveryType,
                    PricePerDelivery = o.DeliveryOption.PricePerDelivery,
                    Products = o.OrdersDetails.Select(d => new ProductsGetUserOrdersDto
                    {
                        ProductId = d.ProductId,
                        ProductName = d.Product.Name,
                        ProductPrice = d.Product.Price,
                        Quantity = d.Quantity
                    }).ToList()
                });


            return ListOfOrders;
        }

        public IEnumerable<GetUserOrdersDto> GetLoggedUserOrders(int userId)
        {        
            var ListOfOrders =
                _context.Orders
                .Where(w => w.UserId == userId)
                .Select(o => new GetUserOrdersDto
                {
                    OrderId = o.Id,
                    UserId = o.UserId,
                    OrderStatus = o.OrderStatus.ToString(),
                    DeliveryType = o.DeliveryOption.DeliveryType,
                    PricePerDelivery = o.DeliveryOption.PricePerDelivery,
                    Products = o.OrdersDetails.Select(d => new ProductsGetUserOrdersDto
                    {
                        ProductId = d.ProductId,
                        ProductName = d.Product.Name,
                        ProductPrice = d.Product.Price,
                        Quantity = d.Quantity
                    }).ToList()
                });

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
