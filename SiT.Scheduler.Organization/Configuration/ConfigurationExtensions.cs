namespace SiT.Scheduler.Organization.Configuration;

using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SiT.Scheduler.Organization.Contracts;

public static class ConfigurationExtensions
{
    public static void ConfigureGraphConnection([NotNull] this IServiceCollection services, [NotNull] IConfiguration configuration)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (configuration is null) throw new ArgumentNullException(nameof(configuration));

        var graphConnectionConfigurationSection = configuration.GetSection(GraphConnectorConfiguration.Section);
        services.Configure<GraphConnectorConfiguration>(graphConnectionConfigurationSection);
        services.AddSingleton<IGraphConnector, GraphConnector>();
    }
}