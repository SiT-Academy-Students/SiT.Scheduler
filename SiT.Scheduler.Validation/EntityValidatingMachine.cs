namespace SiT.Scheduler.Validation;

using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using SiT.Scheduler.Utilities;
using SiT.Scheduler.Utilities.Errors;
using SiT.Scheduler.Utilities.OperationResults;
using SiT.Scheduler.Validation.Contracts;

internal class EntityValidatingMachine<TEntity> : IEntityValidatingMachine
{
    [NotNull]
    private readonly IValidator<TEntity> _validator;
    [NotNull]
    private readonly TEntity _instanceToValidate;

    public EntityValidatingMachine([NotNull] IValidator<TEntity> validator, TEntity instanceToValidate)
    {
        this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
        this._instanceToValidate = instanceToValidate;
    }

    public async Task<IOperationResult> TriggerValidationAsync(CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult();

        var validationResult = await this._validator.ValidateAsync(this._instanceToValidate, cancellationToken);
        if (validationResult.IsValid)
            return operationResult;

        foreach (var error in validationResult.Errors.OrEmptyIfNull().IgnoreNullValues())
        {
            // TODO: Create a separate error class wrapping the `ValidationFailure`.
            operationResult.AddError(new StandardError(error.ErrorMessage));
        }

        return operationResult;
    }
}