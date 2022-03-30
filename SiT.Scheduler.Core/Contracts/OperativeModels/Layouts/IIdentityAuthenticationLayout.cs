namespace SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

using System.Collections.Generic;

public interface IIdentityAuthenticationLayout : ILayout
{
    IIdentityContextualLayout ContextualLayout { get; }
    IReadOnlyCollection<ITenantContextualLayout> Tenants { get; }
}