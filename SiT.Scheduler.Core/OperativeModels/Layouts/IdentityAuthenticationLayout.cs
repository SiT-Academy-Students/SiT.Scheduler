namespace SiT.Scheduler.Core.OperativeModels.Layouts;

using System;
using System.Collections.Generic;
using System.Linq;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using SiT.Scheduler.Utilities;

public record IdentityAuthenticationLayout : BaseLayout, IIdentityAuthenticationLayout
{
    public IdentityAuthenticationLayout(Guid id, IIdentityContextualLayout contextualLayout, IEnumerable<ITenantContextualLayout> tenants) : base(id)
    {
        this.ContextualLayout = contextualLayout ?? throw new ArgumentNullException(nameof(contextualLayout));
        this.Tenants = tenants.OrEmptyIfNull().IgnoreNullValues().ToList().AsReadOnly();
    }

    public IIdentityContextualLayout ContextualLayout { get; }
    public IReadOnlyCollection<ITenantContextualLayout> Tenants { get; }
}