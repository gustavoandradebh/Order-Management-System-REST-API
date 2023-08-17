USE [master]
GO
-- Create the Database Torc
CREATE DATABASE [Torc]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Torc', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Torc.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Torc_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Torc_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 COLLATE SQL_Latin1_General_CP1_CI_AS
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
USE [Torc]
GO
-- Create the Product table
CREATE TABLE Product (
    [ProductID] INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(255) NOT NULL,
    [Price] DECIMAL(10, 2) NOT NULL
)
GO
-- Create the Customer table
CREATE TABLE [Customer] (
    [CustomerID] INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(255) NOT NULL
)
GO

-- Create the Order table
CREATE TABLE [Order] (
    [OrderID] INT PRIMARY KEY IDENTITY(1,1),
    [CustomerID] INT NOT NULL,
	[OrderDate] DATETIME2 CONSTRAINT [DEFAULT_Student_DateOfAdmission] DEFAULT (SYSUTCDATETIME()) NOT NULL
    CONSTRAINT FK_Order_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(ID)
);

-- Create the OrderDetail table
CREATE TABLE [OrderDetail] (
    [OrderDetailID] INT PRIMARY KEY IDENTITY(1,1),
    [OrderID] INT NOT NULL,
    [ProductID] INT NOT NULL,
    [Quantity] INT NOT NULL,
    CONSTRAINT FK_OrderDetail_Order FOREIGN KEY (OrderID) REFERENCES [Order](ID),
    CONSTRAINT FK_OrderDetail_Product FOREIGN KEY (ProductID) REFERENCES Product(ID)
);