namespace SiT.Scheduler.Core.Transformations;
using System;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.DI.Contracts;

public class DataTransformerFactory : IDataTransformerFactory
{
    private readonly IDependenciesAccessor _dependenciesAccessor;

    public DataTransformerFactory(IDependenciesAccessor dependenciesAccessor)
    {
        this._dependenciesAccessor = dependenciesAccessor ?? throw new ArgumentNullException(nameof(dependenciesAccessor));
    }

    public IDataTransformer<TEntity, TLayout> ConstructTransformer<TEntity, TLayout>() => this._dependenciesAccessor.GetDependency<IDataTransformer<TEntity, TLayout>>();
}
