namespace SiT.Scheduler.Resources;

using System;
using System.Reflection;
using System.Resources;

public static class ResourcesHelper
{
    public static ResourceManager Construct(string resourcesFileName)
    {
        if (string.IsNullOrWhiteSpace(resourcesFileName))
            throw new ArgumentNullException(nameof(resourcesFileName));

        var currentAssembly = Assembly.GetExecutingAssembly();
        var resourceManager = new ResourceManager($"SiT.Scheduler.Resources.{resourcesFileName}", currentAssembly);
        return resourceManager;
    }
}
