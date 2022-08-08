using DemoUnitTesting.Controllers;
using DemoUnitTesting.Domain;
using DemoUnitTesting.Domain.Entities;

using Microsoft.AspNetCore.Mvc;

using Moq;

using System.Threading.Tasks;

using Xunit;

namespace DemoUnitTesting.Tests.Controllers.ProductsControllerTests
{
    public class GetTests
    {
        [Fact]
        public async Task When_ResultIsSuccess_Expect_200OkResponse()
        {
            // Arrange
            MockObject mockObject = new MockObject();

            var product = new Product(1, "Cars", "Cars", 10, true);

            mockObject.ProductService
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(product);


            ProductsController controller = new ProductsController(
                mockObject.Mediator.Object,
                mockObject.ProductService.Object);

            // Act

            var actual = await controller.Get(1);

            // Assert

            var okObjectResult = Assert.IsType<OkObjectResult>(actual);

            var productResult = Assert.IsType<Result<Product>>(okObjectResult.Value);

            Assert.Equal(product, productResult.Data);
        }

        [Fact]
        public async Task When_ResultIsError_Expect_400BadRequestResponse()
        {
            // Arrange
            MockObject mockObject = new MockObject();

            var product = new Product(1, "Cars", "Cars", 10, true);

            mockObject.ProductService
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Result<Product>.Failure("ERROR_CODE_X"));


            ProductsController controller = new ProductsController(
                mockObject.Mediator.Object,
                mockObject.ProductService.Object);

            // Act

            var actual = await controller.Get(1);

            // Assert

            var okObjectResult = Assert.IsType<BadRequestObjectResult>(actual);

            var result = Assert.IsType<Result<Product>>(okObjectResult.Value);

            Assert.Equal("ERROR_CODE_X", result.Error);
        }
    }
}
