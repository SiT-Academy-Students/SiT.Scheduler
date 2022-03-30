namespace SiT.Scheduler.Core.Contracts.Authentication;

using JetBrains.Annotations;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

public interface ITenantContext
{
    bool HasTenant { get; }
    ITenantContextualLayout Tenant { get; }

    void SetTenant([NotNull] ITenantContextualLayout tenant);
}