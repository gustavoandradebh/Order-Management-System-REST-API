namespace Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
