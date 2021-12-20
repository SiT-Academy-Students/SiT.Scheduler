namespace SiT.Scheduler.Core.Configuration;

using System;
using Microsoft.Extensions.DependencyInjection;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.Core.Services;
using SiT.Scheduler.Core.Transformations;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Data.Models;
using SiT.Scheduler.Data.Repositories;
using SiT.Scheduler.DI;
using SiT.Scheduler.DI.Contracts;

public static class ServicesConfigurationExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        if (services is null)
            throw new ArgumentNullException(nameof(services));

        // Register all services.
        services.AddScoped<ISongService, SongService>();
        services.AddTransient<IDataTransformerFactory, DataTransformerFactory>();
        services.AddTransient<IDependenciesAccessor, DependenciesAccessor>();
        services.AddSingleton<IDataTransformer<Song, ISongLayout>, SongLayoutTransformer>();

        // Register all repositories.
        services.AddScoped<IRepository<Song>, Repository<Song>>();
    }
}
