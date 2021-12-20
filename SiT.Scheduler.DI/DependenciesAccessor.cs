namespace SiT.Scheduler.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using SiT.Scheduler.DI.Contracts;
using SiT.Scheduler.Utilities;

public class DependenciesAccessor : IDependenciesAccessor
{
    private readonly IServiceProvider _serviceProvider;

    public DependenciesAccessor(IServiceProvider serviceProvider)
    {
        this._serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public object GetDependency(Type type)
    {
        if (type is null) return null;
        return this._serviceProvider.GetService(type);
    }

    public T GetDependency<T>()
    {
        var dependency = this.GetDependency(typeof(T));
        if (dependency is T castedDependency)
            return castedDependency;

        return default;
    }

    public IEnumerable<object> GetDependencies(Type type)
    {
        if (type is null) return Enumerable.Empty<object>();
        var dependencies = this.GetDependency(typeof(IEnumerable<>).MakeGenericType(type)) as IEnumerable<object>;
        return dependencies.OrEmptyIfNull().IgnoreNullValues();
    }
}
