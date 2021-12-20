namespace SiT.Scheduler.DI;
using System;
using SiT.Scheduler.DI.Contracts;

public class DependenciesAccessor : IDependenciesAccessor
{
    private readonly IServiceProvider _serviceProvider;

    public DependenciesAccessor(IServiceProvider serviceProvider)
    {
        this._serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public T GetDependency<T>()
    {
        var dependency=  this._serviceProvider.GetService(typeof(T));
        if (dependency is T castedDependency)
            return castedDependency;

        return default;
    }
}
