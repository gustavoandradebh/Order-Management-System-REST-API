using System.Data;
using Microsoft.Data.SqlClient;

namespace Tests.IntegrationTests;

public class OrderRepositoryTests : IDisposable
{
    private readonly SqlConnection _connection;

    public OrderRepositoryTests()
    {
        string connectionString = "Server=GUS-AVELL\\SQLEXPRESS;Database=Torc;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true";
        _connection = new SqlConnection(connectionString);
        _connection.Open();
    }

    public void Dispose()
    {
        // Clean up resources
        _connection.Close();
        _connection.Dispose();
    }

    [Fact]
    public async Task ExecuteStoreProcedure_ShouldReturnNewOrder()
    {
        // Arrange
        

        // Act
        using (var command = new SqlCommand("dbo.spCreateOrderWithProducts", _connection))
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@CustomerID", 1));
            command.Parameters.Add(new SqlParameter("@OrderDate", DateTime.UtcNow));

            DataTable dt = new DataTable();
            dt.Columns.Add("ProductId", typeof(int));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Rows.Add(1, 10);
            
            SqlParameter orderItems = new SqlParameter("@OrderDetails", System.Data.SqlDbType.Structured)
            {
                TypeName = "dbo.OrderDetailType",
                Value = dt
            };
            command.Parameters.Add(orderItems);

            SqlParameter orderIDParameter = new SqlParameter("@NewOrderID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(orderIDParameter);

            // Execute the stored procedure
            var newOrderId = command.ExecuteNonQuery();

            // Assert
            Assert.True(newOrderId > 0); 
        }
    }
}