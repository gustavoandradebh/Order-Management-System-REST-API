namespace Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public Customer Customer { get; set; } = default!;
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
