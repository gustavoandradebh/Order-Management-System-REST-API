﻿namespace Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
