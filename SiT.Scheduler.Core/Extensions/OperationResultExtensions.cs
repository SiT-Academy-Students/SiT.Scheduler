namespace SiT.Scheduler.Core.Extensions;

using System;
using JetBrains.Annotations;
using SiT.Scheduler.Core.Contracts.Authentication;
using SiT.Scheduler.Utilities.Errors;
using SiT.Scheduler.Utilities.OperationResults;

public static class OperationResultExtensions
{
    public static void ValidateTenantContext<T>([NotNull] this T operationResult, ITenantContext tenantContext)
        where T : IOperationResult
    {
        if (operationResult is null) throw new ArgumentNullException(nameof(operationResult));
        if (tenantContext is null || !tenantContext.HasTenant) operationResult.AddError(new MissingTenantContextError());
    }
}