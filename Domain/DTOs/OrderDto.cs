namespace Domain.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalCost { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = null!;
    }
}
