namespace OrderApi.DTOs
{
    public class PaymentRequestDto
    {
        public string OrderNumber { get; set; }
        public bool IsPaid { get; set; }
    }
}
