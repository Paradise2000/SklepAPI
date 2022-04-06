using System.Text.Json.Serialization;

namespace SklepAPI.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus
    {
        Created,
        Sent,
        Cancelled
    }

    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public List<OrderDetails> OrdersDetails { get; set; }
    }
}
