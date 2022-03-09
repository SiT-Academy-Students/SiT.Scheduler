namespace SiT.Scheduler.Core.Configuration;

using System;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.Core.Services;
using SiT.Scheduler.Core.Transformations;
using SiT.Scheduler.Core.Validation;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Data.Models;
using SiT.Scheduler.Data.Repositories;
using SiT.Scheduler.DI;
using SiT.Scheduler.DI.Contracts;
using SiT.Scheduler.Validation;
using SiT.Scheduler.Validation.Contracts;

public static class ServicesConfigurationExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        if (services is null)
            throw new ArgumentNullException(nameof(services));

        // Register all services.
        services.AddScoped<ISongService, SongService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IPerformerService, PerformerService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddTransient<IDataTransformerFactory, DataTransformerFactory>();
        services.AddTransient<IDependenciesAccessor, DependenciesAccessor>();
        services.AddSingleton<IDataTransformer<Song, ISongLayout>, SongLayoutTransformer>();
        services.AddSingleton<IDataTransformer<Genre, IGenreMinifiedLayout>, GenreMinifiedLayoutTransformer>();
        services.AddSingleton<IDataTransformer<Performer, IPerformerMinifiedLayout>, PerformerMinifiedLayoutTransformer>();
        services.AddSingleton<IDataTransformer<Category, ICategoryMinifiedLayout>, CategoryMinifiedLayoutTransformer>();

        // Register all repositories.
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Register all validation components.
        services.AddScoped<IEntityValidatorFactory, EntityValidatorFactory>();
        services.AddScoped(typeof(IEntityValidator<>), typeof(EntityValidator<>));

        services.AddSingleton<IValidator<ICategoryPrototype>, CategoryPrototypeValidator>();
        services.AddSingleton<IValidator<IGenrePrototype>, GenrePrototypeValidator>();
        services.AddSingleton<IValidator<IPerformerPrototype>, PerformerPrototypeValidator>();
        services.AddSingleton<IValidator<ISongPrototype>, SongPrototypeValidator>();
    }
}
