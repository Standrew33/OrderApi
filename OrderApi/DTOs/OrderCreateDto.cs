namespace OrderApi.DTOs
{
    public class OrderItemDto
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class OrderCreateDto
    {
        public string OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
