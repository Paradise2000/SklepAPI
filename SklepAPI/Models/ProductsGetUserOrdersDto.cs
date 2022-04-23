namespace SklepAPI.Models
{
    public class ProductsGetUserOrdersDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
