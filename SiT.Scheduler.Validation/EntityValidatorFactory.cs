namespace SiT.Scheduler.Validation;
using System;
using JetBrains.Annotations;
using SiT.Scheduler.DI.Contracts;
using SiT.Scheduler.DI.Extensions;
using SiT.Scheduler.Validation.Contracts;

public class EntityValidatorFactory : IEntityValidatorFactory
{
    [NotNull]
    private readonly IDependenciesAccessor _dependenciesAccessor;

    public EntityValidatorFactory([NotNull] IDependenciesAccessor dependenciesAccessor)
    {
        this._dependenciesAccessor = dependenciesAccessor ?? throw new ArgumentNullException(nameof(dependenciesAccessor));
    }

    public IEntityValidator<TEntity> BuildValidator<TEntity>() 
        where TEntity : class 
        => this._dependenciesAccessor.GetRequiredDependency<IEntityValidator<TEntity>>();
}
