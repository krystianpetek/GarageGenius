﻿using GarageGenius.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using GarageGenius.Shared.Infrastructure.Persistance.MsSqlServer;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;

namespace GarageGenius.Modules.Customers.IntegrationTests.Helpers;
public class TestWebApplication<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("IntegrationTests");
        return base.CreateHost(builder);
    }
}
