namespace SiT.Scheduler.DI.Extensions;
using System;
using JetBrains.Annotations;
using SiT.Scheduler.DI.Contracts;

public static class DependenciesAccessorExtensions
{
    public static T GetRequiredDependency<T>([NotNull] this IDependenciesAccessor dependenciesAccessor)
    {
        if (dependenciesAccessor is null)
            throw new ArgumentNullException(nameof(dependenciesAccessor));

        var dependency = dependenciesAccessor.GetDependency<T>();
        if (dependency is null)
            throw new InvalidOperationException($"A dependency of type {typeof(T).Name} could not be successfully accessed.");

        return dependency;
    }
}
