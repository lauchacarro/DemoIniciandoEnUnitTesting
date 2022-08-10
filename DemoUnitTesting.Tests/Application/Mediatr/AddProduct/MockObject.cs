using AutoFixture;

using DemoUnitTesting.Data;

using Microsoft.EntityFrameworkCore;

using System;

namespace DemoUnitTesting.Tests.Application.Mediatr.AddProduct
{
    internal class MockObject
    {
        public MockObject()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;

            ApplicationContext = new ApplicationContext(options);

            Fixture = new();
        }

        public IApplicationContext ApplicationContext { get; set; }

        public Fixture Fixture { get; set; }

    }
}
