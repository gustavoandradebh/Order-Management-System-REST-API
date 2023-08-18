namespace Domain.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string ProductName { get; set; } = default!;

    public decimal Price { get; set; }
}
