using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Service;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public OrderDto CreateOrder(OrderPostDto order)
    {
        ValidateOrder(order);

        var newOrderId = _unitOfWork.Orders.CreateOrder(order);

        return FindById(newOrderId);
    }

    public OrderDto? FindById(int id)
    {
        var order = _unitOfWork.Orders.GetById(id);
        if (order is null)
            return null;

        var orderDto = new OrderDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            OrderDate = order.OrderDate,
            TotalCost = order.OrderDetails.Sum(x => x.UnitPrice * x.Quantity),
            OrderItems = order.OrderDetails.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                ProductId = oi.ProductId,
                ProductName = oi.Product.Name,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList()
        };

        return orderDto;
    }

    private void ValidateOrder(OrderPostDto order)
    {
        // Check if product exists
        var productIds = new HashSet<int>(_unitOfWork.Products.GetAll().Select(p => p.Id));
        foreach (var productId in order.OrderItems.Select(i => i.ProductId))
        {
            if (!productIds.Contains(productId))
                throw new ArgumentException($"Product with id {productId} does not exist");
        }

        // Check if quantity > 0
        foreach (var orderItem in order.OrderItems)
        {
            if (orderItem.Quantity <= 0)
                throw new ArgumentException($"Quantity for product with id {orderItem.ProductId} must be positive");
        }

        // Check if customer exists
        var customer = _unitOfWork.Customers.GetById(order.CustomerId);
        if (customer is null)
            throw new ArgumentException($"Customer with id {order.CustomerId} does not exist");
    }
}
