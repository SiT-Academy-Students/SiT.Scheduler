namespace SiT.Scheduler.Validation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using SiT.Scheduler.DI.Contracts;
using SiT.Scheduler.Utilities;
using SiT.Scheduler.Utilities.OperationResults;
using SiT.Scheduler.Validation.Contracts;

public class EntityValidator<TEntity> : IEntityValidator<TEntity>
    where TEntity : class
{
    [NotNull]
    private readonly IDependenciesAccessor _dependenciesAccessor;

    public EntityValidator([NotNull] IDependenciesAccessor dependenciesAccessor)
    {
        this._dependenciesAccessor = dependenciesAccessor ?? throw new ArgumentNullException(nameof(dependenciesAccessor));
    }

    /// <inheritdoc />
    public async Task<IOperationResult> ValidateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult();

        operationResult.ValidateNotNull(entity);
        if (!operationResult.IsSuccessful)
            return operationResult;

        var validatingMachines = this.ComposeAllValidatingMachines(entity);
        foreach (var validator in validatingMachines.OrEmptyIfNull().IgnoreNullValues())
        {
            var validationResult = await validator.TriggerValidationAsync(cancellationToken).ConfigureAwait(false);
            if (!validationResult.IsSuccessful)
                return operationResult.AppendErrors(validationResult);
        }

        return operationResult;
    }

    private IEnumerable<IEntityValidatingMachine> ComposeAllValidatingMachines(TEntity entity)
    {
        if (entity is null)
            return Enumerable.Empty<IEntityValidatingMachine>();

        var allValidatingMachines = new List<IEntityValidatingMachine>();

        var entityType = entity.GetType();
        AddValidators(entityType);

        var baseType = entityType.BaseType;
        while (baseType != null)
        {
            AddValidators(baseType);
            baseType = baseType.BaseType;
        }

        var interfaces = entityType.GetInterfaces();
        foreach (var implementedInterfaceType in interfaces)
            AddValidators(implementedInterfaceType);

        return allValidatingMachines;

        void AddValidators(Type validatedType)
        {
            var principalType = typeof(TEntity);
            if (!principalType.IsAssignableFrom(validatedType))
                return;

            var validatorType = typeof(IValidator<>).MakeGenericType(validatedType);
            var registeredValidators = this._dependenciesAccessor.GetDependencies(validatorType);
            foreach (var registeredValidator in registeredValidators)
            {
                var validatingMachineType = typeof(EntityValidatingMachine<>).MakeGenericType(validatedType);
                if (Activator.CreateInstance(validatingMachineType, registeredValidator, entity) is IEntityValidatingMachine validatingMachine)
                    allValidatingMachines.Add(validatingMachine);
            }
        }
    }
}
