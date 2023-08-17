USE [Torc]
GO

--Add Customers
INSERT INTO [Customer] ([Name])
     VALUES ('John Doe')
GO
INSERT INTO [Customer] ([Name])
     VALUES ('Michael Jordan')
GO

--Add Products
INSERT INTO [Product] ([Name],[Price])
	 VALUES ('First product', 5.99)
GO
INSERT INTO [Product] ([Name],[Price])
	 VALUES ('Second product', 13.50)
GO
INSERT INTO [Product] ([Name],[Price])
	 VALUES ('Third product', 30)
GO

--Add Orders
INSERT INTO [Order]([CustomerID],[TotalCost])
	VALUES (1, 65.99)
GO

INSERT INTO [OrderDetail]([OrderID],[ProductID],[Quantity])
     VALUES (1, 1, 1)
GO
INSERT INTO [OrderDetail]([OrderID],[ProductID],[Quantity])
     VALUES (1, 3, 2)
GO

INSERT INTO [Order]([CustomerID],[TotalCost])
	VALUES (2, 43.50)
GO
INSERT INTO [OrderDetail]([OrderID],[ProductID],[Quantity])
     VALUES (2, 2, 1)
GO
INSERT INTO [OrderDetail]([OrderID],[ProductID],[Quantity])
     VALUES (2, 3, 1)
GO




