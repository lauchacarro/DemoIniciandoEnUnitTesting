using AutoFixture.Xunit2;

using DemoUnitTesting.Application.Mediatr.AddProduct;
using DemoUnitTesting.Controllers;
using DemoUnitTesting.Domain;

using Microsoft.AspNetCore.Mvc;

using Moq;

using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace DemoUnitTesting.Tests.Controllers.ProductsControllerTests
{
    public class CreateTests
    {
        [Theory, AutoData]
        public async Task When_ResultIsSuccess_Expect_200OkResponse(AddProductResponse response)
        {
            // Arrange

            MockObject mockObject = new();

            mockObject.Mediator
            .Setup(m => m.Send(It.IsAny<AddProductRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);


            ProductsController controller = new ProductsController(
                mockObject.Mediator.Object,
                mockObject.ProductService.Object);

            // Act

            var actual = await controller.Create(It.IsAny<AddProductRequest>());

            // Assert

            var okObjectResult = Assert.IsType<OkObjectResult>(actual);

            Assert.IsType<Result<AddProductResponse>>(okObjectResult.Value);

        }


        [Theory, AutoData]
        public async Task When_ResultIsError_Expect_400BadRequestResponse(string errorCode)
        {
            // Arrange

            MockObject mockObject = new MockObject();

            mockObject.Mediator
            .Setup(m => m.Send(It.IsAny<AddProductRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(errorCode);


            ProductsController controller = new ProductsController(
                mockObject.Mediator.Object,
                mockObject.ProductService.Object);

            // Act

            var actual = await controller.Create(It.IsAny<AddProductRequest>());

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actual);

            var productResult = Assert.IsType<Result<AddProductResponse>>(badRequestResult.Value);

            Assert.Equal(errorCode, productResult.Error);
        }
    }
}
