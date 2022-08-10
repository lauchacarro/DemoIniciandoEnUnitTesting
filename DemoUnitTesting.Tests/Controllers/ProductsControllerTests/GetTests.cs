using AutoFixture.Xunit2;

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
        [Theory, AutoData]
        public async Task When_ResultIsSuccess_Expect_200OkResponse(Product product)
        {
            // Arrange
            MockObject mockObject = new MockObject();

            mockObject.ProductService
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(product);


            ProductsController controller = new ProductsController(
                mockObject.Mediator.Object,
                mockObject.ProductService.Object);

            // Act

            var actual = await controller.Get(It.IsAny<int>());

            // Assert

            var okObjectResult = Assert.IsType<OkObjectResult>(actual);

            var productResult = Assert.IsType<Result<Product>>(okObjectResult.Value);

            Assert.Equal(product, productResult.Data);
        }

        [Theory, AutoData]
        public async Task When_ResultIsError_Expect_400BadRequestResponse(string errorCode)
        {
            // Arrange
            MockObject mockObject = new MockObject();

            mockObject.ProductService
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(errorCode);


            ProductsController controller = new ProductsController(
                mockObject.Mediator.Object,
                mockObject.ProductService.Object);

            // Act

            var actual = await controller.Get(It.IsAny<int>());

            // Assert

            var okObjectResult = Assert.IsType<BadRequestObjectResult>(actual);

            var result = Assert.IsType<Result<Product>>(okObjectResult.Value);

            Assert.Equal(errorCode, result.Error);
        }
    }
}
