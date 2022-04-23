namespace SklepAPI.Entities
{
    public class DeliveryOption
    {
        public int Id { get; set; }
        public string DeliveryType { get; set; }
        public double PricePerDelivery { get; set; }
        public virtual List<Order> Order { get; set; }
    }
}
