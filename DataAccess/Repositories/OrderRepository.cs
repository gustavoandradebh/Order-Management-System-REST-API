using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF.Repositories
{
    public class OrderRepository: GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext context) : base(context) { }

        public Order GetById(int orderId)
        {
            var order = _context.Orders
                .Include(order => order.OrderDetails)
                    .ThenInclude(orderDetail => orderDetail.Product)
                .Include(order => order.Customer)
                .FirstOrDefault(order => order.Id == orderId);

            return order;
        }

        public Order CreateOrder(OrderPostDto order)
        {
            SqlParameter orderItems = new SqlParameter("@OrderItems", System.Data.SqlDbType.Structured)
            {
                TypeName = "dbo.OrderItems",
                Value = order.OrderItems.ToArray()
            };

            _context.Orders.FromSqlRaw("EXECUTE dbo.CreateOrder @customerId, @orderDate, @orderItems", 
                new SqlParameter("@customerId", order.CustomerId),
                new SqlParameter("@orderDate", order.OrderDate),
                orderItems);

            return null;
            
        }

    }
}
