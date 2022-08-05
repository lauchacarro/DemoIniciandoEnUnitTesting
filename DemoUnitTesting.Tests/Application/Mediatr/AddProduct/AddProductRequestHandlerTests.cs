using DemoUnitTesting.Application.Mediatr.AddProduct;
using DemoUnitTesting.Domain.Entities;

using System.Threading.Tasks;

using Xunit;

namespace DemoUnitTesting.Tests.Application.Mediatr.AddProduct
{
    public class AddProductRequestHandlerTests
    {
        [Fact]
        public async Task Add_ValidProduct_ReturnSuccess()
        {
            // Arrange

            MockObject mockObject = new();

            var handler = new AddProductRequestHandler(mockObject.ApplicationContext);

            // Act

            var actual = await handler.Handle(new AddProductRequest("Cars", "Cars", 2), default);

            // Assert

            Assert.True(actual.Succeeded);
        }

        [Fact]
        public async Task Add_EmptyName_ReturnErrorCode()
        {
            // Arrange

            MockObject mockObject = new();

            var handler = new AddProductRequestHandler(mockObject.ApplicationContext);

            // Act

            var actual = await handler.Handle(new AddProductRequest(string.Empty, string.Empty, 2), default);

            // Assert

            Assert.False(actual.Succeeded);
            Assert.Equal("ERROR_CODE_01", actual.Error);
        }

        [Fact]
        public async Task Add_ShortDescription_ReturnErrorCode()
        {
            // Arrange

            MockObject mockObject = new();

            var handler = new AddProductRequestHandler(mockObject.ApplicationContext);

            // Act

            var actual = await handler.Handle(new AddProductRequest("Car", "A", 2), default);

            // Assert

            Assert.False(actual.Succeeded);
            Assert.Equal("ERROR_CODE_02", actual.Error);
        }

        [Fact]
        public async Task Add_ZeroPrice_ReturnErrorCode()
        {
            // Arrange

            MockObject mockObject = new();

            var handler = new AddProductRequestHandler(mockObject.ApplicationContext);

            // Act

            var actual = await handler.Handle(new AddProductRequest("Cars", null, 0), default);

            // Assert

            Assert.False(actual.Succeeded);
            Assert.Equal("ERROR_CODE_03", actual.Error);
        }

        [Fact]
        public async Task Add_AlreadyExistsName_ReturnErrorCode()
        {
            // Arrange
            const string PRODUCT_NAME = "Cars";

            MockObject mockObject = new();

            mockObject.ApplicationContext.Products.Add(new Product(PRODUCT_NAME, null, 1, true));
            await mockObject.ApplicationContext.SaveChangesAsync(default);

            var handler = new AddProductRequestHandler(mockObject.ApplicationContext);

            // Act

            var actual = await handler.Handle(new AddProductRequest(PRODUCT_NAME, null, 1), default);

            // Assert

            Assert.False(actual.Succeeded);
            Assert.Equal("ERROR_CODE_04", actual.Error);
        }
    }
}
