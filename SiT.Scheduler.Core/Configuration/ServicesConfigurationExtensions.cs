namespace SiT.Scheduler.Core.Configuration;

using System;
using Microsoft.Extensions.DependencyInjection;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Data.Models;
using SiT.Scheduler.Data.Repositories;

public static class ServicesConfigurationExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        if (services is null)
            throw new ArgumentNullException(nameof(services));

        // Register all repositories.
        services.AddScoped<IRepository<Song>, Repository<Song>>();
    }
}
