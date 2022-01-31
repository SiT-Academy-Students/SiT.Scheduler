namespace SiT.Scheduler.Tests.Equalization;
using Microsoft.Extensions.DependencyInjection;
using TryAtSoftware.Equalizer.Core.Interfaces;
using Xunit;

public static class EqualizationConfiguration
{
    public static void RegisterEqualizationProfiles(this IServiceCollection serviceCollection)
    {
        Assert.NotNull(serviceCollection);
        serviceCollection.AddSingleton<IEqualizationProfile, SongEqualizationProfile>();
    }
}
