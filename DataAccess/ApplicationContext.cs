using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF;

public class ApplicationContext: DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<OrderDetail> OrderDetails { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasData(
                       new Customer { Id = 1, Name = "John Doe", Email = "john@doe.com" });
    }
}
