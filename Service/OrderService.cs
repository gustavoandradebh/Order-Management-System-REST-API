using Domain.DTOs;
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
        _unitOfWork.Orders.CreateOrder(order);
        return null;
    }

    public OrderDto? FindById(int id)
    {
        var order = _unitOfWork.Orders.GetById(id);
        if(order is null)
            return null;

        var orderDto = new OrderDto
        {
            Id = order.Id,
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
}
