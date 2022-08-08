using DemoUnitTesting.Application.Mediatr.DeleteProduct;
using DemoUnitTesting.Domain.Entities;

using System.Threading.Tasks;

using Xunit;

namespace DemoUnitTesting.Tests.Application.Mediatr.DeleteProduct
{
    [Trait("Products - DeleteProduct", value: "Unit tests for DeleteProduct use case")]

    public class DeleteProductRequestHandlerTests
    {
        [Fact]
        public async Task When_ProductExists_Expect_ReturnSuccess()
        {
            // Arrange

            const string PRODUCT_NAME = "Cars";

            MockObject mockObject = new();

            mockObject.ApplicationContext.Products.Add(new Product(PRODUCT_NAME, null, 1, true));

            await mockObject.ApplicationContext.SaveChangesAsync(default);


            var handler = new DeleteProductRequestHandler(mockObject.ApplicationContext);

            // Act

            var actual = await handler.Handle(new DeleteProductRequest(1), default);

            // Assert

            Assert.True(actual.Succeeded);
        }

        [Fact]
        public async Task When_ProductNotExists_Expect_ReturnErrorCode()
        {
            // Arrange

            MockObject mockObject = new();

            var handler = new DeleteProductRequestHandler(mockObject.ApplicationContext);

            // Act

            var actual = await handler.Handle(new DeleteProductRequest(1), default);

            // Assert

            Assert.False(actual.Succeeded);
            Assert.Equal("ERROR_CODE_D1", actual.Error);
        }
    }
}
