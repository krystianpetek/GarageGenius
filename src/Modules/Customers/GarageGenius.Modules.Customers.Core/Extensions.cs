﻿using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GarageGenius.Modules.Customers.Api")]
[assembly: InternalsVisibleTo("GarageGenius.Modules.Customers.Application")]
[assembly: InternalsVisibleTo("GarageGenius.Modules.Customers.Infrastructure")]
namespace GarageGenius.Modules.Customers.Core;

internal static class Extensions
{
    public static IServiceCollection AddCustomersCore(this IServiceCollection services)
    {
        return services;
    }
}