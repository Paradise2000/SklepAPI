using SklepAPI.Entities;

namespace SklepAPI.Models
{
    public class NewOrderDto
    {
        public int UserId { get; set; }
        public int DeliveryOptionId { get; set; }
        public List<ProductsListNewOrderDto> ListOfProductsId { get; set; }
    }
}
