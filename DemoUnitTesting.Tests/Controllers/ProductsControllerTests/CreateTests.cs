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
        [Fact]
        public async Task Create_ValidProduct_ReturnOkResponse()
        {
            // Arrange

            MockObject mockObject = new MockObject();

            var productResponse = new AddProductResponse(1, "Cars", "Cars", 10, true);

            mockObject.Mediator
            .Setup(m => m.Send(It.IsAny<AddProductRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(productResponse);


            ProductsController controller = new ProductsController(
                mockObject.Mediator.Object,
                mockObject.ProductService.Object);

            // Act

            var actual = await controller.Create(It.IsAny<AddProductRequest>());

            // Assert

            var okObjectResult = Assert.IsType<OkObjectResult>(actual);

            var productResult = Assert.IsType<Result<AddProductResponse>>(okObjectResult.Value);

        }


        [Fact]
        public async Task Create_ErrorCode_ReturnBadRequest()
        {
            // Arrange

            MockObject mockObject = new MockObject();

            mockObject.Mediator
            .Setup(m => m.Send(It.IsAny<AddProductRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync("ERROR_CODE_X");


            ProductsController controller = new ProductsController(
                mockObject.Mediator.Object,
                mockObject.ProductService.Object);

            // Act

            var actual = await controller.Create(It.IsAny<AddProductRequest>());

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actual);

            var productResult = Assert.IsType<Result<AddProductResponse>>(badRequestResult.Value);

            Assert.Equal("ERROR_CODE_X", productResult.Error);
        }
    }
}
