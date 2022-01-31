namespace SiT.Scheduler.API.Configuration;

using System;
using Microsoft.Extensions.DependencyInjection;
using SiT.Scheduler.API.Contracts.Factories;
using SiT.Scheduler.API.Factories;

public static class FactoryConfigurationExtensions
{
    public static void RegisterFactories(this IServiceCollection services)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));

        services.AddSingleton<ISongFactory, SongFactory>();
        services.AddSingleton<IGenreFactory, GenreFactory>();
    }
}
