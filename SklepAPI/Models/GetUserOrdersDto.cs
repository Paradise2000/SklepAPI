namespace SklepAPI.Models
{
    public class GetUserOrdersDto
    {
        public int OrderId { get; set; }
        public int OrderStatus { get; set; }
        public List<ProductsGetUserOrdersDto> Products { get; set; }
    }
}
