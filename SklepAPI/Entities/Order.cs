namespace SklepAPI.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime OrderDate { get; set; }
        public int OrderStatus { get; set; }

        public List<OrderDetails> OrdersDetails { get; set; }
    }
}
