using DemoUnitTesting.Application.Mediatr.DeleteProduct;
using DemoUnitTesting.Domain.Entities;

using System.Threading.Tasks;

using Xunit;

namespace DemoUnitTesting.Tests.Application.Mediatr.DeleteProduct
{
    public class DeleteProductRequestHandlerTests
    {
        [Fact]
        public async Task Delete_ExistingProduct_ReturnSuccess()
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
        public async Task Delete_InexistingProduct_ReturnErrorCode()
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
