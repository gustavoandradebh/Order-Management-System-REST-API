using System.Data;

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

        public int CreateOrder(OrderPostDto order)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductId", typeof(int));
            dt.Columns.Add("Quantity", typeof(int));

            foreach (var item in order.OrderItems)
            {
                dt.Rows.Add(item.ProductId, item.Quantity);
            }

            SqlParameter orderItems = new SqlParameter("@OrderDetails", System.Data.SqlDbType.Structured)
            {
                TypeName = "dbo.OrderDetailType",
                Value = dt
            };
            SqlParameter orderIDParameter = new SqlParameter("@NewOrderID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            _context.Database.ExecuteSqlRaw("EXECUTE dbo.spCreateOrderWithProducts @CustomerId, @OrderDate, @OrderDetails, @NewOrderID OUTPUT",
                       new SqlParameter("@CustomerID", order.CustomerId),
                       new SqlParameter("@OrderDate", DateTime.UtcNow),
                       orderItems,
                       orderIDParameter);

            var newOrderID = Convert.ToInt32(orderIDParameter.Value);
            return newOrderID;
        }

    }
}
