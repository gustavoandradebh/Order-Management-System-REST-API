namespace Domain.DTOs
{
    public class OrderItemPostDto
    {
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
