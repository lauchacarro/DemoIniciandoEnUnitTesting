using DemoUnitTesting.Application.Services;
using DemoUnitTesting.Domain.Entities;

using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;

using Xunit;

namespace DemoUnitTesting.Tests.Application.Services.ProductServiceTests
{
    [Trait("Products - GetById", "Unit tests for GetById use case")]

    public class GetByIdAsyncTests
    {
        [Fact]
        public async Task When_ProductExists_Expect_ReturnSuccess()
        {
            // Arrange
            MockObject mockObject = new MockObject();

            // Seteo información al Mock de IHttpContextAccessor

            var context = new DefaultHttpContext();

            context.Request.Headers["Tenant-ID"] = "abcd";

            mockObject.HttpContextAccessor.Setup(_ => _.HttpContext).Returns(context);


            // Seteo información al Mock de ApplicationContext

            mockObject.ApplicationContext.Products.Add(new Product("Cars", null, 1, true));

            await mockObject.ApplicationContext.SaveChangesAsync(default);


            ProductService service = new ProductService(
                mockObject.ApplicationContext,
                mockObject.HttpContextAccessor.Object);

            // Act

            var actual = await service.GetByIdAsync(1);

            // Assert

            Assert.True(actual.Succeeded);
        }

        [Fact]
        public async Task When_ProductNotExists_Expect_ReturnErrorCode()
        {
            // Arrange
            MockObject mockObject = new MockObject();

            ProductService service = new ProductService(
                mockObject.ApplicationContext,
                mockObject.HttpContextAccessor.Object);

            // Act

            var actual = await service.GetByIdAsync(1);

            // Assert

            Assert.False(actual.Succeeded);
            Assert.Equal("ERROR_CODE_G1", actual.Error);
        }
    }
}
