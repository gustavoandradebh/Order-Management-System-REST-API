using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;

using Moq;

using Service;

namespace Tests.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public OrderServiceTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public void CreateOrder_WhenProductNotExists_ShouldThrowError()
        {
            // Arrange
            var service = new OrderService(_unitOfWork.Object);
            var order = new OrderPostDto
            {
                OrderItems = new List<OrderItemPostDto>
                {
                    new OrderItemPostDto
                    {
                        ProductId = 1,
                        Quantity = 1
                    }
                },
                CustomerId = 1

            };
            _unitOfWork.Setup(x => x.Products.GetAll()).Returns(new List<Product>().AsQueryable());

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => service.CreateOrder(order));
            Assert.Equal("Product with id 1 does not exist", exception.Message);
        }

        [Fact]
        public void CreateOrder_WhenQuantityIsZero_ShouldThrowError()
        {
            // Arrange
            var service = new OrderService(_unitOfWork.Object);
            var order = new OrderPostDto
            {
                OrderItems = new List<OrderItemPostDto>
                {
                    new OrderItemPostDto
                    {
                        ProductId = 1,
                        Quantity = 0
                    }
                },
                CustomerId = 1

            };
            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Price = 10
            };
            _unitOfWork.Setup(x => x.Products.GetAll()).Returns(new List<Product> { product }.AsQueryable());

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => service.CreateOrder(order));
            Assert.Equal("Quantity for product with id 1 must be positive", exception.Message);
        }

        [Fact]
        public void CreateOrder_WhenCustomerNotExists_ShouldThrowError()
        {
            // Arrange
            var service = new OrderService(_unitOfWork.Object);
            var order = new OrderPostDto
            {
                OrderItems = new List<OrderItemPostDto>
                {
                    new OrderItemPostDto
                    {
                        ProductId = 1,
                        Quantity = 10
                    }
                },
                CustomerId = 999

            };
            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Price = 10
            };
            _unitOfWork.Setup(x => x.Products.GetAll()).Returns(new List<Product> { product }.AsQueryable());
            _unitOfWork.Setup(x => x.Customers.GetById(order.CustomerId)).Returns((Customer)null);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => service.CreateOrder(order));
            Assert.Equal("Customer with id 999 does not exist", exception.Message);
        }
    }
}