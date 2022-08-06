﻿using DemoUnitTesting.Data;

using Microsoft.EntityFrameworkCore;

using System;

namespace DemoUnitTesting.Tests.Application.Mediatr.DeleteProduct;

internal class MockObject
{
    public MockObject()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
         .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
         .Options;

        ApplicationContext = new ApplicationContext(options);
    }

    public IApplicationContext ApplicationContext { get; set; }
}
