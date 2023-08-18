using Domain.DTOs;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Order GetById(int orderId);

        int CreateOrder(OrderPostDto order);
    }
}
