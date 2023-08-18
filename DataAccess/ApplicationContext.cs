using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF;

public class ApplicationContext : IdentityDbContext<IdentityUser>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<OrderDetail> OrderDetails { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Price).HasColumnType("decimal(18,2)");

            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(p => p.UnitPrice).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OrderDate).HasColumnType("datetime2");
            
            entity.HasOne(s => s.Customer)
                .WithMany(g => g.Orders)
                .HasForeignKey(s => s.CustomerId);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Phone).HasMaxLength(50);

            entity.HasMany(s => s.Orders)
                .WithOne(g => g.Customer)
                .HasForeignKey(s => s.CustomerId);
        });

        SeedInitialData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private void SeedInitialData(ModelBuilder modelBuilder)
    {
        #region Initial Seed
        var customer = new Customer { Id = 1, Name = "John Doe", Email = "john@doe.com", Phone = "963-547-4363" };
        modelBuilder.Entity<Customer>().HasData(customer);

        var product = new Product { Id = 1, Name = "Dummy Product", Price = 5.5m };
        modelBuilder.Entity<Product>().HasData(product);

        var product2 = new Product { Id = 2, Name = "Second Product", Price = 10m };
        modelBuilder.Entity<Product>().HasData(product2);

        var order = new Order
        {
            Id = 1,
            CustomerId = customer.Id,
            OrderDate = DateTime.Now,
        };
        modelBuilder.Entity<Order>().HasData(order);

        modelBuilder.Entity<OrderDetail>().HasData(
            new List<OrderDetail>
                {
                        new OrderDetail { Id = 1, OrderId = order.Id, ProductId = product.Id, Quantity = 2, UnitPrice = product.Price },
                        new OrderDetail { Id = 2, OrderId = order.Id, ProductId = product2.Id, Quantity = 1, UnitPrice = product2.Price }
                });
        #endregion
    }

}
