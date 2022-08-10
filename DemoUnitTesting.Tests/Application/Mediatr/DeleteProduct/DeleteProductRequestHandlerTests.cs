using AutoFixture;


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

            MockObject mockObject = new();

            var product = mockObject.Fixture.Create<Product>();

            mockObject.ApplicationContext.Products.Add(product);

            await mockObject.ApplicationContext.SaveChangesAsync(default);

            DeleteProductRequest request = new(product.Id);

            var handler = new DeleteProductRequestHandler(mockObject.ApplicationContext);

            // Act

            var actual = await handler.Handle(request, default);

            // Assert

            Assert.True(actual.Succeeded);
        }

        [Fact]
        public async Task When_ProductNotExists_Expect_ReturnErrorCode()
        {
            // Arrange

            MockObject mockObject = new();

            DeleteProductRequest request = new(mockObject.Fixture.Create<int>());

            var handler = new DeleteProductRequestHandler(mockObject.ApplicationContext);

            // Act

            var actual = await handler.Handle(request, default);

            // Assert

            Assert.False(actual.Succeeded);
            Assert.Equal("ERROR_CODE_D1", actual.Error);
        }
    }
}
