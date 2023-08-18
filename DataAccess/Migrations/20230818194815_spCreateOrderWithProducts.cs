using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.EF.Migrations
{
    /// <inheritdoc />
    public partial class spCreateOrderWithProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE TYPE dbo.OrderDetailType AS TABLE
                        (
                            ProductID INT,
                            Quantity INT
                        );
                        GO
                        CREATE OR ALTER PROCEDURE spCreateOrderWithProducts
                            @CustomerID INT,
	                        @OrderDate DATETIME2,
                            @OrderDetails AS dbo.OrderDetailType READONLY,
	                        @NewOrderID INT OUTPUT
                        AS
                        BEGIN
                            -- Calculate total cost
                            DECLARE @TotalCost DECIMAL(10, 2)
    
	                        -- I'm not using it since the sum is being made on runtime. 
	                        -- Just leaving it here because the assignment asked to.
                            SELECT @TotalCost = SUM(p.Price * od.Quantity)
                            FROM @OrderDetails od
                            JOIN Products p ON od.ProductID = p.ID

                            -- Insert into Orders
                            INSERT INTO Orders (CustomerID, OrderDate)
                            VALUES (@CustomerID, @OrderDate)

                            -- Get the newly inserted OrderID
                            SET @NewOrderID  = SCOPE_IDENTITY()

                            -- Insert into OrderDetail for each product
                            INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice)
                            SELECT @NewOrderID, od.ProductID, od.Quantity, (p.Price * od.Quantity)
                            FROM @OrderDetails od
                            JOIN Products p ON od.ProductID = p.ID
	
	                        SELECT @NewOrderID AS NewOrderID;
                        END";

            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
