namespace SiT.Scheduler.Core.Configuration;

using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SiT.Scheduler.StorageManagement;
using SiT.Scheduler.StorageManagement.CloudinaryImpl;
using SiT.Scheduler.StorageManagement.CloudinaryImpl.Configuration;
using SiT.Scheduler.StorageManagement.Configuration;
using SiT.Scheduler.StorageManagement.Contracts;

public static class StorageManagementConfigurationExtensions
{
    public static void SetupStorageManagement([NotNull] this IServiceCollection services, [CanBeNull] StorageManagementConfiguration storageManagementConfiguration, [NotNull] IConfiguration configuration)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (configuration is null) throw new ArgumentNullException(nameof(configuration));

        if (storageManagementConfiguration is null) services.SetupFallbackStorageManagement();
        else if (string.Equals(storageManagementConfiguration.Provider, "cloudinary", StringComparison.InvariantCultureIgnoreCase)) services.SetupCloudinary(configuration);
        else throw new InvalidOperationException("Invalid storage management provider");
    }

    private static void SetupCloudinary([NotNull] this IServiceCollection services, [NotNull] IConfiguration configuration)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (configuration is null) throw new ArgumentNullException(nameof(configuration));

        var cloudinaryConfiguration = configuration.GetSection(CloudinaryConfiguration.Section);
        services.Configure<CloudinaryConfiguration>(cloudinaryConfiguration);
        services.AddSingleton<IStorageManager, CloudinaryStorageManager>();
    }

    private static void SetupFallbackStorageManagement([NotNull] this IServiceCollection services)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        services.AddSingleton<IStorageManager, FallbackStorageManager>();
    }
}