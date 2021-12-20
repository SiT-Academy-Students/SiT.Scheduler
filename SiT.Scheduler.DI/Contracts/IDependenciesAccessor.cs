namespace SiT.Scheduler.DI.Contracts;
public interface IDependenciesAccessor
{
    T GetDependency<T>();
}
