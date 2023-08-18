using Domain.DTOs;

namespace Domain.Interfaces
{
    public interface IOrderService
    {
        OrderDto? FindById(int id);

        OrderDto CreateOrder(OrderPostDto order);
    }
}
