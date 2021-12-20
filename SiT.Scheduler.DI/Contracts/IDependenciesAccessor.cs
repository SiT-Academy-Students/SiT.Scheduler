namespace SiT.Scheduler.DI.Contracts;

using System;
using System.Collections.Generic;

public interface IDependenciesAccessor
{
    object GetDependency(Type type);
    T GetDependency<T>();
    IEnumerable<object> GetDependencies(Type type);
}
