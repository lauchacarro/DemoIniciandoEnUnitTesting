using DemoUnitTesting.Application.Services;

using MediatR;

using Moq;

namespace DemoUnitTesting.Tests.Controllers.ProductsControllerTests
{
    internal class MockObject
    {
        public MockObject()
        {
            Mediator = new();
            ProductService = new();
        }

        public Mock<IMediator> Mediator { get; set; }
        public Mock<IProductService> ProductService { get; set; }

    }
}
