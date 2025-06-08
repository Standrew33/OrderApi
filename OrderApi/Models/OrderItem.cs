using System.ComponentModel.DataAnnotations;

namespace OrderApi.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }
        public int OrderId { get; set; }
    }
}
