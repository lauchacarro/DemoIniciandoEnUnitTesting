using AutoFixture;

using DemoUnitTesting.Data;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Moq;

using System;

namespace DemoUnitTesting.Tests.Application.Services.ProductServiceTests
{
    internal class MockObject
    {
        public MockObject()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;

            ApplicationContext = new ApplicationContext(options);

            HttpContextAccessor = new Mock<IHttpContextAccessor>();
            Fixture = new();
        }

        public IApplicationContext ApplicationContext { get; set; }
        public Mock<IHttpContextAccessor> HttpContextAccessor { get; set; }

        public Fixture Fixture { get; set; }
    }
}
