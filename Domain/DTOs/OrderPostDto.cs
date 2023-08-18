namespace Domain.DTOs;

public class OrderPostDto
{
    public int CustomerId { get; set; }
    public List<OrderItemPostDto> OrderItems { get; set; } = null!;
}
