using Domain.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebApi.Controllers;

namespace Tests.Services
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderService> _orderService;
        private readonly Mock<ILogger<OrderController>> _logger;

        public OrderControllerTests()
        {
            _orderService = new Mock<IOrderService>();
            _logger = new Mock<ILogger<OrderController>>();
        }

        [Fact]
        public async Task CreateOrder_EverythingCorrect_ReturnsOk()
        {
            // Arrange
            var orderPost = new OrderPostDto
            {
                CustomerId = 1,
                OrderItems = new List<OrderItemPostDto>
                {
                    new OrderItemPostDto
                    {
                        ProductId = 1,
                        Quantity = 1
                    }
                }
            };

            var orderDto = new OrderDto
            {
                Id = 1,
                CustomerId = orderPost.CustomerId,
                OrderDate = DateTime.Now,
                TotalCost = 10,
                OrderItems = new List<OrderItemDto>
                {
                    new OrderItemDto
                    {
                        Id = 1,
                        ProductId = 1,
                        ProductName = "Test",
                        Quantity = 1,
                        UnitPrice = 10
                    }
                }
            };
            _orderService.Setup(r => r.CreateOrder(orderPost)).Returns(orderDto);

            // Act
            var controller = new OrderController(_orderService.Object, _logger.Object);

            // Assert
            var result = controller.CreateOrder(orderPost);
            var createdObjectResult = result as CreatedResult;
            Assert.NotNull(createdObjectResult);
            Assert.True(createdObjectResult.StatusCode == 201);

            var dto = createdObjectResult.Value as OrderDto;
            Assert.Equivalent(orderDto, dto);
        }

        [Fact]
        public async Task CreateOrder_ValidationFails_ReturnsBadRequest()
        {
            // Arrange
            var orderPost = new OrderPostDto
            {
                CustomerId = 1,
                OrderItems = new List<OrderItemPostDto>
                {
                    new OrderItemPostDto
                    {
                        ProductId = 1,
                        Quantity = 1
                    }
                }
            };

            var orderDto = new OrderDto
            {
                Id = 1,
                CustomerId = orderPost.CustomerId,
                OrderDate = DateTime.Now,
                TotalCost = 10,
                OrderItems = new List<OrderItemDto>
                {
                    new OrderItemDto
                    {
                        Id = 1,
                        ProductId = 1,
                        ProductName = "Test",
                        Quantity = 1,
                        UnitPrice = 10
                    }
                }
            };
            _orderService.Setup(r => r.CreateOrder(orderPost)).Throws(new ArgumentException("Product with id 1 does not exist"));

            // Act
            var controller = new OrderController(_orderService.Object, _logger.Object);

            // Assert
            var result = controller.CreateOrder(orderPost);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.True(badRequestResult.StatusCode == 400);
        }
    }
}