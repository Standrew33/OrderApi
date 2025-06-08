using System.ComponentModel.DataAnnotations;

namespace OrderApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string OrderNumber { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public OrderStatus Status { get; set; } = OrderStatus.New;
        public List<OrderItem> Items { get; set; } = new();
    }
}
