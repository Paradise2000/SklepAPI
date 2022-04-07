using SklepAPI.Entities;
using System.Text.Json.Serialization;


namespace SklepAPI.Models
{
    public class GetUserOrdersDto
    {
        public int OrderId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? UserId { get; set; }
        public string OrderStatus { get; set; }
        public List<ProductsGetUserOrdersDto> Products { get; set; }
    }
}
