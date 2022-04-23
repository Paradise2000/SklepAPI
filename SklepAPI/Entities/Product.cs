namespace SklepAPI.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public int ImagePath { get; set; }

        public virtual List<OrderDetails> OrdersDetails { get; set; }
    }
}
