using DemoUnitTesting.Application.Mediatr.AddProduct;
using DemoUnitTesting.Domain.Entities;

using System.Threading.Tasks;

using Xunit;

namespace DemoUnitTesting.Tests.Application.Mediatr.AddProduct
{
    [Trait("Products - AddProduct", value: "Unit tests for AddProduct use case")]

    public class AddProductRequestHandlerTests
    {
        [Fact]
        public async Task When_ValidRequest_Expect_ReturnSuccess()
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
        public async Task When_NameIsEmpty_Expect_ReturnErrorCode()
        {
            // Arrange

            MockObject mockObject = new();

            var handler = new AddProductRequestHandler(mockObject.ApplicationContext);

            // Act

            var actual = await handler.Handle(new AddProductRequest(string.Empty, string.Empty, 2), default);

            // Assert

            Assert.False(actual.Succeeded);
            Assert.Equal("ERROR_CODE_A1", actual.Error);
        }

        [Fact]
        public async Task When_DescriptionIsShort_Expect_ReturnErrorCode()
        {
            // Arrange

            MockObject mockObject = new();

            var handler = new AddProductRequestHandler(mockObject.ApplicationContext);

            // Act

            var actual = await handler.Handle(new AddProductRequest("Car", "A", 2), default);

            // Assert

            Assert.False(actual.Succeeded);
            Assert.Equal("ERROR_CODE_A2", actual.Error);
        }

        [Fact]
        public async Task When_PriceIsEqualsOrLessThanZero_Expect_ReturnErrorCode()
        {
            // Arrange

            MockObject mockObject = new();

            var handler = new AddProductRequestHandler(mockObject.ApplicationContext);

            // Act

            var actual = await handler.Handle(new AddProductRequest("Cars", null, 0), default);

            // Assert

            Assert.False(actual.Succeeded);
            Assert.Equal("ERROR_CODE_A3", actual.Error);
        }

        [Fact]
        public async Task When_NameAlreadyExist_Expect_ReturnErrorCode()
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
            Assert.Equal("ERROR_CODE_A4", actual.Error);
        }
    }
}
